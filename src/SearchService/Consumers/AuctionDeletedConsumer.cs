using AutoMapper;
using Contracts.Models;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class AuctionDeletedConsumer : IConsumer<AuctionDelete>
    {
         private readonly IMapper _mapper;

        public AuctionDeletedConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<AuctionDelete> context)
        {
           await DB.DeleteAsync<Item>(context.Message.Id);
        }
    }
}