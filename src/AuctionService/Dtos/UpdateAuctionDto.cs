namespace AuctionService;

public class UpdateAuctionDto
{
    public string Status { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Tags { get; set; } = string.Empty;
    public int ReservePrice { get; set; } = 0;
    public DateTime EndDate { get; set; }
}
