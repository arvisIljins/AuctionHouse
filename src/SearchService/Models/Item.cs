
using MongoDB.Entities;

namespace SearchService.Models
{
    public class Item : Entity
    {
    public int ReservePrice { get; set; } = 0;
    public string Seller { get; set; } = string.Empty;
    public string Winner { get; set; } = string.Empty;
    public int? SoldAmount { get; set; }
    public int? CurrentHightBid { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }  
    public DateTime EndDate { get; set; } 
    public string Status { get; set; }
    public string ImageUrl { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Tags { get; set; }
    }
}