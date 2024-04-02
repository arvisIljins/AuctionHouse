using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Models;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class AuctionFinishedConsumer : IConsumer<AuctionFinished>
    {
        public async Task Consume(ConsumeContext<AuctionFinished> context)
        {
            var auction = await DB.Find<Item>().OneAsync(context.Message.AuctionId);
            var saveData = auction is not null && context.Message.ItemSold;
            if(saveData && auction is not null)
            {
                auction.Winner = context.Message.Winner;
                auction.SoldAmount = context.Message.Amount;
                auction.Status = auction.SoldAmount > auction.ReservePrice
            ? Status.Finished.ToString() : Status.ReserveNotMet.ToString() ;
            await auction.SaveAsync();
            }

        }
    }
}