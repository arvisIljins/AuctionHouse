using AuctionService.Models;

namespace AuctionService.Services.AuctionService
{
    public interface IAuctionService
    {
        Task<ServiceResponse<List<AuctionDto>>> GetAllAuctions();        
        Task<ServiceResponse<AuctionDto>>CreateAuction(CreateAuctionDto auctionDto);   
        Task<ServiceResponse<AuctionDto>>UpdateAuction(UpdateAuctionDto auctionDto);   
        Task<ServiceResponse<string>>DeleteAuction(Guid id);     
    }
}