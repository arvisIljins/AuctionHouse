using AuctionService.Models;

namespace AuctionService.Services.AuctionService
{
    public interface IAuctionService
    {
        Task<ServiceResponse<List<AuctionDto>>> GetAllAuctions();        
    }
}