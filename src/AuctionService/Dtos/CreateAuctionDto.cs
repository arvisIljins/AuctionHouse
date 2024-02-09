using System.ComponentModel.DataAnnotations;

namespace AuctionService;

public class CreateAuctionDto
{   
    [Required]
    public string Status { get; set; } = string.Empty;
    [Required]
    public string ImageUrl { get; set; } = string.Empty;
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    [Required]
    public string Tags { get; set; } = string.Empty;
    [Required]
    public int ReservePrice { get; set; } = 0;
    [Required]
    public DateTime EndDate { get; set; }
}
