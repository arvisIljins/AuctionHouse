
using AuctionService.Data;
using AuctionService.Repositories.AuctionsRepository;
using Contracts.Models;
using MassTransit;

namespace AuctionService.Consumers
{
    public class BidPlacedConsumer : IConsumer<BidPlaced>
    {
        private readonly AuctionDbContext _context;
        private readonly IAuctionsRepository _auctionRepository;

        public BidPlacedConsumer(AuctionDbContext context, IAuctionsRepository auctionRepository)
        {
            _context = context;
            _auctionRepository = auctionRepository;
        }
        public async Task Consume(ConsumeContext<BidPlaced> context)
        {
            var auction = await _context.Auctions.FindAsync(new Guid(context.Message.AuctionId));

            if(auction is null) return;

            if(auction.CurrentHightBid is null || context.Message.Status.Contains("Accepted") && context.Message.Amount > auction.CurrentHightBid)
            {
                auction.CurrentHightBid = context.Message.Amount;
                _auctionRepository.UpdateAuctionAsync(auction);
                await _auctionRepository.SaveChangesAsync();
            }
        }
    }
}