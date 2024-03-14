using AuctionService.Services.AuctionService;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions(string? date)
    {
        return Ok(await _auctionService.GetAllAuctions(date));
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto auctionDto)
    {
        return Ok(await _auctionService.CreateAuction(auctionDto));
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<AuctionDto>> UpdateAuction(UpdateAuctionDto auctionDto)
    {
        return Ok(await _auctionService.UpdateAuction(auctionDto));
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> DeleteAuction(Guid id)
    {
        return Ok(await _auctionService.DeleteAuction(id));
    }

    
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
    {
        return Ok(await _auctionService.GetAuctionById(id));
    }
}
