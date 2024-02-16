using AutoMapper;
using Contracts.Models;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class AuctionUpdateConsumer: IConsumer<AuctionUpdated>
    {
         private readonly IMapper _mapper;

        public AuctionUpdateConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<AuctionUpdated> context)
        {
           var item = _mapper.Map<Item>(context.Message);

            var result  = await DB.Update<Item>()
            .Match(a => a.ID == context.Message.Id)
            .ModifyOnly(x => new {
                x.ImageUrl,
                x.Title,
                x.Description,
                x.Tags,
                x.ReservePrice,
                x.EndDate
            }, item)
            .ExecuteAsync();
        }
    }
}