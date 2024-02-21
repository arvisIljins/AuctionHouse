
using Contracts.Models;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class BidPlacedConsumer : IConsumer<BidPlaced>
    {
        public async Task Consume(ConsumeContext<BidPlaced> context)
        {
           var auction = await DB.Find<Item>().OneAsync(context.Message.AuctionId);

            if(auction is null) return;

            if(auction.CurrentHightBid is null || context.Message.Status.Contains("Accepted") && context.Message.Amount > auction.CurrentHightBid)
            {
                auction.CurrentHightBid = context.Message.Amount;
                await auction.SaveAsync();
            }
        }
    }
}