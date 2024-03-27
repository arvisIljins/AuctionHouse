using AuctionService.Data;
using Grpc.Core;

namespace AuctionService.Services.GrpcAuctionService
{
    public class GrpcAuctionService : GrpcAuction.GrpcAuctionBase
    {
        private readonly AuctionDbContext _dbContext;

        public GrpcAuctionService(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<GrpcAuctionResponse> GetAuction(GetAuctionRequest request, 
            ServerCallContext context) 
        {
            Console.WriteLine("==> Received Grpc request for auction");

            var auction = await _dbContext.Auctions.FindAsync(Guid.Parse(request.Id)) 
                ?? throw new RpcException(new Grpc.Core.Status(StatusCode.NotFound, "Not found"));
                
            var response = new GrpcAuctionResponse
            {
                Auction = new GrpcAuctionModel
                {
                    EndDate = auction.EndDate.ToString(),
                    Id = auction.Id.ToString(),
                    ReservePrice = auction.ReservePrice,
                    Seller = auction.Seller
                }
            };

            return response;
        }
    }
}