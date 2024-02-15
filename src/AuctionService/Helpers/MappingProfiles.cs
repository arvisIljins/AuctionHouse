using AutoMapper;
using Contracts.Models;

namespace AuctionService;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Auction, AuctionDto>().IncludeMembers(x => x.Item);
        CreateMap<Item, AuctionDto>();
        CreateMap<CreateAuctionDto, Auction>().ForMember(x => x.Item, o => o.MapFrom(s => s));
        CreateMap<CreateAuctionDto, Item>();
        CreateMap<UpdateAuctionDto, Auction>();
        CreateMap<AuctionDto, AuctionCreated>();
    }
}
