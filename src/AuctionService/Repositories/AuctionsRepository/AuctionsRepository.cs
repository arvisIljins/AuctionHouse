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
        public async Task<List<Auction>> GetAuctionsAsync()
        {
            return await _context.Auctions.Include(x => x.Item).ToListAsync();
        }
    }
}