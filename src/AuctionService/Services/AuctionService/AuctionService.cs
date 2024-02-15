using AuctionService.Repositories.AuctionsRepository;
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
        public AuctionService(IMapper mapper, IAuctionsRepository auctionRepository, IPublishEndpoint publishEndpoint)
        {
            _mapper = mapper;
            _auctionRepository = auctionRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<ServiceResponse<AuctionDto>> CreateAuction(CreateAuctionDto auctionDto)
        {
            var serviceResponse = new ServiceResponse<AuctionDto>();
            try
            {
                var auction = _mapper.Map<Auction>(auctionDto);
                auction.Seller = "Test";
                var newAuction = await _auctionRepository.CreateAuction(auction);
                var auctionForResponse = _mapper.Map<AuctionDto>(newAuction);
                serviceResponse.Data = auctionForResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Error retrieving auctions. Error - {ex}";
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<string>> DeleteAuction(Guid id)
        {
            var serviceResponse = new ServiceResponse<string>();
            try
            {
                var auctions = await _auctionRepository.DeleteAuction(id);
                serviceResponse.Message = $"Auction deleted - id {id}";
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
                var newAuction = await _auctionRepository.UpdateAuction(auctionDto);
                serviceResponse.Data = _mapper.Map<AuctionDto>(newAuction);
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