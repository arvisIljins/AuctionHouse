using AutoMapper;
using BiddingService.DTOs;
using BiddingService.Models;
using Contracts.Models;

namespace AuctionService;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Bid, BidsDto>();
        CreateMap<Bid, BidPlaced>();
    }
}
