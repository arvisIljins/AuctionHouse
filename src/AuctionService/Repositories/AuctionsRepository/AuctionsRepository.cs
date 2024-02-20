using AuctionService.Data;
using AuctionService.Services.IdentityService;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Repositories.AuctionsRepository
{
    public class AuctionsRepository : IAuctionsRepository
    {
    private readonly AuctionDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint  _publishEndpoint;
    private readonly IIdentityService _identityService;
        public AuctionsRepository(AuctionDbContext context, IMapper mapper, IPublishEndpoint publishEndpoint, IIdentityService identityService)
        {
            _context = context;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _identityService = identityService;
        }

        public async Task<Auction> CreateAuction(Auction auction)
        {
            _context.Auctions.Add(auction);
            var auctionForResponse = _mapper.Map<AuctionDto>(auction);
            var auctionForServiceBus =  _mapper.Map<AuctionCreated>(auctionForResponse);
            await _publishEndpoint.Publish(auctionForServiceBus);
            await _context.SaveChangesAsync();
            return auction; 
        }

        public async Task<bool> DeleteAuction(Guid id)
        {
             var auction = await _context.Auctions.FindAsync(id) 
                ?? throw new Exception($"{id} - not found item with such id");
            var currentUser = _identityService.GetUserName();
            if(auction.Seller != currentUser) throw new Exception($"{currentUser} - not permission for this auction");
                _context.Auctions.Remove(auction);
            await _publishEndpoint.Publish<AuctionDelete>(new { Id = auction.Id });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<AuctionDto>> GetAuctionsAsync(string? date)
        {
            var query = _context.Auctions.OrderBy(x => x.CreatedAt) .AsQueryable();

            if(!string.IsNullOrEmpty(date)){
                query = query.Where(x => x.UpdatedAt.CompareTo(DateTime.Parse(date).ToUniversalTime()) > 0);
            }

            return await query.ProjectTo<AuctionDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<Auction> UpdateAuction(UpdateAuctionDto newAuction)
        {  
            var updatedAuction = await _context.Auctions
                .Include(c => c.Item)
                .FirstOrDefaultAsync(c => c.Id == newAuction.Id);
            var currentUser = _identityService.GetUserName();

            if(updatedAuction?.Seller != currentUser) throw new Exception($"This auction is forbidden");
        
            if(updatedAuction is null || updatedAuction.Item is null)  throw new Exception($"Auction Id is incorrect");

            updatedAuction.Item.ImageUrl = newAuction.ImageUrl ?? updatedAuction.Item.ImageUrl;
            updatedAuction.Item.Title = newAuction.Title ?? updatedAuction.Item.Title;
            updatedAuction.Item.Description = newAuction.Description ?? updatedAuction.Item.Description;
            updatedAuction.Item.Tags = newAuction.Tags ?? updatedAuction.Item.Tags;
            updatedAuction.ReservePrice = newAuction.ReservePrice ?? updatedAuction.ReservePrice;
            var publishAuction = _mapper.Map<AuctionUpdated>(updatedAuction);
            await _publishEndpoint.Publish(publishAuction);
            await _context.SaveChangesAsync();
            return updatedAuction; 
        }
    }
}