using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionService;

[Table("Items")]
public class Item
{
    public Guid Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Tags { get; set; } = string.Empty;

    // nav properties
    public Auction? Auction { get; set; }
    public Guid AuctionId { get; set; }
}
