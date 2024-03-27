using Contracts.Models;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Hubs;

namespace NotificationService.Consumers 
{
    public class AuctionFinishedConsumer : IConsumer<AuctionFinished>
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        public AuctionFinishedConsumer(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task Consume(ConsumeContext<AuctionFinished> context)
        {
            await _hubContext.Clients.All.SendAsync("AuctionFinished", context.Message);
        }  
    }
}