
using BiddingService.Models;

namespace BiddingService.Repositories
{
    public interface IBidsRepository
    {
        Task<Auction?> GetAuctionsById(string id);  
        Task<List<Bid>> GetBidsByAuctionId(string auctionId);
    }
}