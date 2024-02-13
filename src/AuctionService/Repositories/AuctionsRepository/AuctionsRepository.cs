using AuctionService.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Repositories.AuctionsRepository
{
    public class AuctionsRepository : IAuctionsRepository
    {
    private readonly AuctionDbContext _context;
    private readonly IMapper _mapper;
        public AuctionsRepository(AuctionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Auction> CreateAuction(Auction auction)
        {
            _context.Auctions.Add(auction);
            await _context.SaveChangesAsync();
            return auction; 
        }

        public async Task<bool> DeleteAuction(Guid id)
        {
             var auction = await _context.Auctions.FindAsync(id) 
                ?? throw new Exception($"{id} - not found item with such id");
                _context.Auctions.Remove(auction);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Auction>> GetAuctionsAsync()
        {
            return await _context.Auctions.Include(x => x.Item).ToListAsync();
        }

        public async Task<Auction> UpdateAuction(UpdateAuctionDto newAuction)
        {
        
            var updatedAuction = await _context.Auctions
                .Include(c => c.Item)
                .FirstOrDefaultAsync(c => c.Id == newAuction.Id);
        
            if(updatedAuction is null || updatedAuction.Item is null)  throw new Exception($"Characters Id is incorrect");

            updatedAuction.Item.ImageUrl = newAuction.ImageUrl ?? updatedAuction.Item.ImageUrl;
            updatedAuction.Item.Title = newAuction.Title ?? updatedAuction.Item.Title;
            updatedAuction.Item.Description = newAuction.Description ?? updatedAuction.Item.Description;
            updatedAuction.Item.Tags = newAuction.Tags ?? updatedAuction.Item.Tags;
            updatedAuction.ReservePrice = newAuction.ReservePrice ?? updatedAuction.ReservePrice;
           
            await _context.SaveChangesAsync();
            return updatedAuction; 
        }
    }
}