using AuctionService;
using BiddingService.Models;
using Grpc.Net.Client;

namespace BiddingService.Services.GrpcClients;

    public class GrpcAuctionClient
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<GrpcAuctionClient> _logger;
        public GrpcAuctionClient(IConfiguration configuration, ILogger<GrpcAuctionClient> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public Auction? GetAuction(string id)
        {
            _logger.LogInformation("Calling GRPC Service");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcAuction"] ?? "");
            var client = new GrpcAuction.GrpcAuctionClient(channel);
            var request = new GetAuctionRequest{Id = id};
            try
            {
                var reply = client.GetAuction(request);
                var auction = new Auction
                {
                    ID = reply.Auction.Id,
                    AuctionEnds = DateTime.Parse(reply.Auction.EndDate),
                    Seller = reply.Auction.Seller,
                    ReservePrice = reply.Auction.ReservePrice
                };

                return auction;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }