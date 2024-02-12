using AuctionService.Models;

namespace AuctionService.Services.AuctionService
{
    public interface IAuctionService
    {
        Task<ServiceResponse<List<AuctionDto>>> GetAllAuctions();        
        Task<ServiceResponse<AuctionDto>>CreateAuctions(CreateAuctionDto createAuctionDto);     
    }
}