using AuctionService.Models;
using AuctionService.Repositories.AuctionsRepository;
using AutoMapper;

namespace AuctionService.Services.AuctionService
{
    public class AuctionService : IAuctionService
    {
        private readonly IMapper _mapper;
        private readonly IAuctionsRepository _auctionRepository;
        public AuctionService(IMapper mapper, IAuctionsRepository auctionRepository)
        {
            _mapper = mapper;
            _auctionRepository = auctionRepository;
        }

        public async Task<ServiceResponse<AuctionDto>> CreateAuctions(CreateAuctionDto auctionDto)
        {
            var serviceResponse = new ServiceResponse<AuctionDto>();
            try
            {
                var auction = _mapper.Map<Auction>(auctionDto);
                auction.Seller = "Test";
                var newAuction = await _auctionRepository.CreateAuctions(auction);
                serviceResponse.Data = _mapper.Map<AuctionDto>(newAuction);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Error retrieving auctions. Error - {ex}";
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<AuctionDto>>> GetAllAuctions()
        {
            var serviceResponse = new ServiceResponse<List<AuctionDto>>();
            try
            {
                var auctions = await _auctionRepository.GetAuctionsAsync();
                serviceResponse.Data = _mapper.Map<List<AuctionDto>>(auctions);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Error retrieving auctions. Error - {ex}";
            }
            return serviceResponse;
        }    
    }
}