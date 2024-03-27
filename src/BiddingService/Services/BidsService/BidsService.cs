using AutoMapper;
using BiddingService.DTOs;
using BiddingService.Models;
using BiddingService.Repositories;
using BiddingService.Services.GrpcClients;
using BidsService.Services.IdentityService;
using Contracts.Models;
using MassTransit;
using MassTransit.Initializers;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;

namespace BiddingService.Services.BidsService
{
    public class BidsService : IBidsService
    {
        private readonly IBidsRepository _bidsRepository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint  _publishEndpoint;
        private readonly GrpcAuctionClient _grpcAuctionClient;
        public BidsService(IBidsRepository bidsRepository, IIdentityService identityService, IMapper mapper, IPublishEndpoint publishEndpoint, GrpcAuctionClient grpcAuctionClient)
        {
            _bidsRepository = bidsRepository;
            _identityService = identityService;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _grpcAuctionClient = grpcAuctionClient;
        }

        public async Task<List<BidsDto>> GetBidsByAuctionId(string auctionId)
        {
            var bids =  await _bidsRepository.GetBidsByAuctionId(auctionId);
            var bidsForResponse = bids.Select(_mapper.Map<BidsDto>);
            return bidsForResponse.ToList();
        }

        public async Task<ActionResult<BidsDto>> PlaceBid(string auctionId, int amount)
        {
            try 
            {
                var auction = await _bidsRepository.GetAuctionsById(auctionId);

                if (auction is null) 
                {
                    
                    auction = _grpcAuctionClient.GetAuction(auctionId);

                    if(auction == null)  return new NotFoundObjectResult("Auction not found");
                }

                var currentUserName = _identityService.GetUserName();
                if (auction.Seller == currentUserName)
                {
                    return new BadRequestObjectResult("You can't bid on your own auction");
                }

                var bid = new Bid 
                {
                    Amount = amount,
                    AuctionId = auctionId,
                    Bidder = currentUserName
                };

                if(auction.AuctionEnds < DateTime.UtcNow)
                {
                    bid.Status = BidStatus.Finished;
                } 
                else 
                {
                    var highBid = await DB.Find<Bid>()
                        .Match(a => a.AuctionId == auctionId)
                        .Sort(b => b.Descending(x => x.Amount))
                        .ExecuteFirstAsync();

                    if(highBid is not null && amount > highBid.Amount || highBid is null)
                    {
                        bid.Status = amount > auction.ReservePrice 
                            ? BidStatus.Accepted 
                            : BidStatus.AcceptedBellowReserve;
                    }

                    if(highBid is not null && bid.Amount <= highBid.Amount)
                    {
                        bid.Status = BidStatus.TooLow;
                    }
                } 

            await DB.SaveAsync(bid);

            var publishBid = _mapper.Map<BidPlaced>(bid);
            await _publishEndpoint.Publish(publishBid);

            return new OkObjectResult(_mapper.Map<BidsDto>(bid));
   
            } 
            catch(Exception ex) 
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}