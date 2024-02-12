using AuctionService.Data;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Repositories.AuctionsRepository
{
    public class AuctionsRepository : IAuctionsRepository
    {
    private readonly AuctionDbContext _context;
    public AuctionsRepository(AuctionDbContext context)
    {
            _context = context;
    }

        public async Task<Auction> CreateAuctions(Auction auction)
        {
            _context.Auctions.Add(auction);
            await _context.SaveChangesAsync();
            return auction; 
        }

        public async Task<List<Auction>> GetAuctionsAsync()
        {
            return await _context.Auctions.Include(x => x.Item).ToListAsync();
        }
    }
}