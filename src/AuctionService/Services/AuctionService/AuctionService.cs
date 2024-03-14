using AuctionService.Repositories.AuctionsRepository;
using AuctionService.Services.IdentityService;
using AutoMapper;
using Contracts.Models;
using MassTransit;

namespace AuctionService.Services.AuctionService
{
    public class AuctionService : IAuctionService
    {
        private readonly IMapper _mapper;
        private readonly IAuctionsRepository _auctionRepository;
        private readonly IPublishEndpoint  _publishEndpoint;
        private readonly IIdentityService _identityService;
        public AuctionService(IMapper mapper, IAuctionsRepository auctionRepository, IPublishEndpoint publishEndpoint, IIdentityService identityService)
        {
            _mapper = mapper;
            _auctionRepository = auctionRepository;
            _publishEndpoint = publishEndpoint;
            _identityService = identityService;
        }

        public async Task<ServiceResponse<AuctionDto>> CreateAuction(CreateAuctionDto auctionDto)
        {
            var serviceResponse = new ServiceResponse<AuctionDto>();
            try
            {
                var auction = _mapper.Map<Auction>(auctionDto);
                auction.Seller = _identityService.GetUserName();
                _auctionRepository.CreateAuction(auction);
                var auctionForResponse = _mapper.Map<AuctionDto>(auction);
                var auctionForServiceBus =  _mapper.Map<AuctionCreated>(auctionForResponse);
                await _publishEndpoint.Publish(auctionForServiceBus);
                var saveChanges = await _auctionRepository.SaveChangesAsync();
                if(!saveChanges){
                    throw new Exception($"Auction is not saved");
                }
                serviceResponse.Data = auctionForResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Error creating auctions. Error - {ex}";
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<string>> DeleteAuction(Guid id)
        {
            var serviceResponse = new ServiceResponse<string>();
            try
            {
                var auction = await _auctionRepository.GetAuctionsEntityByIdAsync(id) 
                ?? throw new Exception($"{id} - not found item with such id");
                var currentUser = _identityService.GetUserName();
                if(auction.Seller != currentUser) throw new Exception($"{currentUser} - not permission for this auction");
                _auctionRepository.DeleteAuction(auction);
                await _publishEndpoint.Publish<AuctionDelete>(new { Id = auction.Id });
                await _auctionRepository.SaveChangesAsync();
                serviceResponse.Message = $"Auction deleted - id {id}";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Error deleting auction. Error - {ex}";
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<AuctionDto>> GetAuctionById(Guid id)
        {
            var serviceResponse = new ServiceResponse<AuctionDto>();
            try
            {
                var auction = await _auctionRepository.GetAuctionsByIdAsync(id) 
                ?? throw new Exception($"{id} - not found item with such id");
        
                serviceResponse.Data = auction;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Error deleting auction. Error - {ex}";
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<AuctionDto>>> GetAllAuctions(string? date)
        {
            var serviceResponse = new ServiceResponse<List<AuctionDto>>();
            try
            {
                var auctions = await _auctionRepository.GetAuctionsAsync(date);
                serviceResponse.Data = auctions;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Error retrieving auctions. Error - {ex}";
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<AuctionDto>> UpdateAuction(UpdateAuctionDto auctionDto)
        {
            var serviceResponse = new ServiceResponse<AuctionDto>();
            try
            {
            var updatedAuction = await _auctionRepository.GetAuctionsEntityByIdAsync(auctionDto.Id); 
            var currentUser = _identityService.GetUserName();

            if(updatedAuction?.Seller != currentUser) throw new Exception($"This auction is forbidden");
        
            if(updatedAuction is null || updatedAuction.Item is null) throw new Exception($"Auction Id is incorrect");

            updatedAuction.Item.ImageUrl = auctionDto.ImageUrl ?? updatedAuction.Item.ImageUrl;
            updatedAuction.Item.Title = auctionDto.Title ?? updatedAuction.Item.Title;
            updatedAuction.Item.Description = auctionDto.Description ?? updatedAuction.Item.Description;
            updatedAuction.Item.Tags = auctionDto.Tags ?? updatedAuction.Item.Tags;
            updatedAuction.ReservePrice = auctionDto.ReservePrice ?? updatedAuction.ReservePrice;
            var publishAuction = _mapper.Map<AuctionUpdated>(updatedAuction);
            await _publishEndpoint.Publish(publishAuction);
            await _auctionRepository.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<AuctionDto>(auctionDto);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Error - {ex}";
            }
            return serviceResponse;
        }
    }
}