using AuctionService.Services.AuctionService;
using Microsoft.AspNetCore.Mvc;

namespace AuctionService;

[ApiController]
[Route("api/auctions")]
public class AuctionsController : ControllerBase
{
    private readonly IAuctionService _auctionService;
    public AuctionsController(IAuctionService auctionService)
    {
        _auctionService = auctionService;
    }

    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions()
    {
        return Ok(await _auctionService.GetAllAuctions());
    }

    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuctions(CreateAuctionDto auctionDto)
    {
        return Ok(await _auctionService.CreateAuctions(auctionDto));
    }
}
