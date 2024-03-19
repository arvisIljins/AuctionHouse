using BiddingService.DTOs;
using BiddingService.Models;
using Microsoft.AspNetCore.Mvc;

namespace BiddingService.Services.BidsService
{
    public interface IBidsService
    {
        Task<ActionResult<BidsDto>> PlaceBid(string auctionId, int amount);  
        Task<List<BidsDto>> GetBidsByAuctionId(string auctionId);
    }
}