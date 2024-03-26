
using BiddingService.Models;
using Contracts.Models;
using MassTransit;
using MongoDB.Entities;

namespace BidsService.Services.CheckAuctionFinished
{
    public class CheckAuctionFinished : BackgroundService
    {
        public ILogger<CheckAuctionFinished> _logger;
        public IServiceProvider _serviceProvider;
        public CheckAuctionFinished(ILogger<CheckAuctionFinished> logger, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting check for finished auctions");
            stoppingToken.Register(() => _logger.LogInformation("Auction check is stopping"));

            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckAuction(stoppingToken);
                await Task.Delay(5000, stoppingToken);
            }
        }

        private async Task CheckAuction(CancellationToken stoppingToken)
        {
            var finishedAuctions = await DB.Find<Auction>()
            .Match(x => x.AuctionEnds <= DateTime.UtcNow)
            .Match(x => !x.Finished)
            .ExecuteAsync(stoppingToken);

            if(finishedAuctions.Count == 0) return;

            _logger.LogInformation("Fount {count} auctions that have completed", finishedAuctions.Count);

            using var scope = _serviceProvider.CreateScope();
            var endpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

            foreach (var auction in finishedAuctions)
            {
                auction.Finished = true;

                await auction.SaveAsync(null, stoppingToken);
                var winningBid = await DB.Find<Bid>()
                .Match(a => a.AuctionId == auction.ID)
                .Match(b => b.Status == BidStatus.Accepted)
                .Sort(x => x.Descending(s => s.Amount))
                .ExecuteFirstAsync(stoppingToken);

                await endpoint.Publish(new AuctionFinished 
                {
                    ItemSold = winningBid != null,
                    AuctionId = auction?.ID ?? "",
                    Winner = winningBid?.Bidder ?? "",
                    Amount = winningBid?.Amount,
                    Seller = auction?.Seller ?? ""
                }, stoppingToken);
            }
        }
    }
}