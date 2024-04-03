using BiddingService.DTOs;
using BiddingService.Models;
using BiddingService.Repositories;
using BiddingService.Services.BidsService;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<BidsDto>> PlaceBid(string auctionId, int amount)
        {
            var response = await _bidsService.PlaceBid(auctionId, amount);
            if(!response.Success){
                return BadRequest(response);
            }
            return Ok(response);
        }     

        [HttpGet("{auctionId}")]
        public async Task<ActionResult<List<Bid>>> GetBidsForAuction(string auctionId)
        {     
            var response = await _bidsService.GetBidsByAuctionId(auctionId);

            if(!response.Success){
                return BadRequest(response);
            }
            return Ok(response);
        } 
    }
}