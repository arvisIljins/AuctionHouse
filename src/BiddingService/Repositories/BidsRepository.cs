using BiddingService.Models;
using MongoDB.Entities;

namespace BiddingService.Repositories
{
    public class BidsRepository : IBidsRepository
    {
        public async Task<Auction?> GetAuctionsById(string id)
        {
            return await DB.Find<Auction>().OneAsync(id);
        }

        public async Task<List<Bid>> GetBidsByAuctionId(string auctionId)
        {
            return await DB.Find<Bid>()
                    .Match(a => a.AuctionId == auctionId)
                    .Sort(b => b.Descending(a => a.BidTime))
                    .ExecuteAsync();
        }
    }
}