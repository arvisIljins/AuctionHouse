using Contracts.Models;

namespace AuctionService.Services.AuctionService
{
    public interface IAuctionService
    {
        Task<ServiceResponse<List<AuctionDto>>> GetAllAuctions(string? date);        
        Task<ServiceResponse<AuctionDto>>CreateAuction(CreateAuctionDto auctionDto);   
        Task<ServiceResponse<AuctionDto>>UpdateAuction(UpdateAuctionDto auctionDto);   
        Task<ServiceResponse<string>>DeleteAuction(Guid id);     
    }
}