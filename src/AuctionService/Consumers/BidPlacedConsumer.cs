
using AuctionService.Data;
using Contracts.Models;
using MassTransit;

namespace AuctionService.Consumers
{
    public class BidPlacedConsumer : IConsumer<BidPlaced>
    {
        private readonly AuctionDbContext _context;

        public BidPlacedConsumer(AuctionDbContext context)
        {
            _context = context;
        }
        public async Task Consume(ConsumeContext<BidPlaced> context)
        {
            var auction = await _context.Auctions.FindAsync(context.Message.AuctionId);

            if(auction is null) return;

            if(auction.CurrentHightBid is null || context.Message.Status.Contains("Accepted") && context.Message.Amount > auction.CurrentHightBid)
            {
                auction.CurrentHightBid = context.Message.Amount;
                await _context.SaveChangesAsync();
            }
        }
    }
}