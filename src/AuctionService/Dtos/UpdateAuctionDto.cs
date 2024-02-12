namespace AuctionService;

public class UpdateAuctionDto
{
    public Guid Id { get; set; }
    public string? ImageUrl { get; set; } 
    public string? Title { get; set; } 
    public string? Description { get; set; } 
    public string? Tags { get; set; } 
    public int? ReservePrice { get; set; } = 0;
    public DateTime? EndDate { get; set; }
}
