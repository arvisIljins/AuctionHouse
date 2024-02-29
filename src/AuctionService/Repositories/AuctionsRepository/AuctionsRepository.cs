using AuctionService.Data;
using AuctionService.Services.IdentityService;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MassTransit;
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

        public void CreateAuction(Auction auction)
        {
            _context.Auctions.Add(auction);
        }

        public void DeleteAuction(Auction auction)
        {
            _context.Auctions.Remove(auction);
        }

        public async Task<List<AuctionDto>> GetAuctionsAsync(string? date)
        {
            var query = _context.Auctions.OrderBy(x => x.CreatedAt) .AsQueryable();

            if(!string.IsNullOrEmpty(date)){
                query = query.Where(x => x.UpdatedAt.CompareTo(DateTime.Parse(date).ToUniversalTime()) > 0);
            }

            return await query.ProjectTo<AuctionDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<AuctionDto> GetAuctionsByIdAsync(Guid id)
        {
           return await  _context.Auctions.ProjectTo<AuctionDto>(_mapper.ConfigurationProvider)
           .FirstOrDefaultAsync(x => x.Id == id) ?? new AuctionDto();
        }

        public async Task<Auction> GetAuctionsEntityByIdAsync(Guid id)
        {
            return await _context.Auctions
                .ProjectTo<Auction>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == id) ?? new Auction();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return   await _context.SaveChangesAsync() > 0;
        }
    }
}