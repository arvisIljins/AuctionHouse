using BiddingService.DTOs;
using Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace BiddingService.Services.BidsService
{
    public interface IBidsService
    {
        Task<ServiceResponse<BidsDto>> PlaceBid(string auctionId, int amount);  
        Task<ServiceResponse<List<BidsDto>>> GetBidsByAuctionId(string auctionId);
    }
}