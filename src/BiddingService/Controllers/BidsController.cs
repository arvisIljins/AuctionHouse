using BiddingService.DTOs;
using BiddingService.Models;
using BiddingService.Repositories;
using BiddingService.Services.BidsService;
using Microsoft.AspNetCore.Mvc;

namespace BiddingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class BidsController : ControllerBase
    {
        private readonly IBidsService _bidsService;
        private readonly IBidsRepository _bidsRepository;
        public BidsController(IBidsService bidsService, IBidsRepository bidsRepository)
        {
            _bidsService = bidsService;
            _bidsRepository = bidsRepository;
        }

        [HttpPost]
        public async Task<ActionResult<BidsDto>> PlaceBid(string auctionId, int amount)
        {
            return Ok(await _bidsService.PlaceBid(auctionId, amount));
        }     

        [HttpGet("auctionId")]
        public async Task<ActionResult<List<Bid>>> GetBidsForAuction(string auctionId)
        {     
            return Ok(await _bidsService.GetBidsByAuctionId(auctionId));
        } 
    }
}