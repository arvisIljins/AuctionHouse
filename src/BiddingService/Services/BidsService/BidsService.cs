using AutoMapper;
using BiddingService.DTOs;
using BiddingService.Models;
using BiddingService.Repositories;
using BidsService.Services.IdentityService;
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
        public BidsService(IBidsRepository bidsRepository, IIdentityService identityService, IMapper mapper)
        {
            _bidsRepository = bidsRepository;
            _identityService = identityService;
            _mapper = mapper;
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
                    return new NotFoundObjectResult("Auction not found");
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
                    bid.BidStatus = BidStatus.Finished;
                } 
                else 
                {
                    var highBid = await DB.Find<Bid>()
                        .Match(a => a.AuctionId == auctionId)
                        .Sort(b => b.Descending(x => x.Amount))
                        .ExecuteFirstAsync();

                    if(highBid is not null && amount > highBid.Amount || highBid is null)
                    {
                        bid.BidStatus = amount > auction.ReservePrice 
                            ? BidStatus.Accepted 
                            : BidStatus.AcceptedBellowReserve;
                    }

                    if(highBid is not null && bid.Amount <= highBid.Amount)
                    {
                        bid.BidStatus = BidStatus.TooLow;
                    }
                } 

            await DB.SaveAsync(bid);

            return new OkObjectResult(_mapper.Map<BidsDto>(bid));
   
            } 
            catch(Exception ex) 
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}