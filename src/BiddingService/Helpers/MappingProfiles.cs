using AutoMapper;
using BiddingService.DTOs;
using BiddingService.Models;

namespace AuctionService;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Bid, BidsDto>();
    }
}
