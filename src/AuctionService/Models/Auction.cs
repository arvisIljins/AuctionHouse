﻿namespace AuctionService;

public class Auction
{
    public Guid Id { get; set; }
    public int ReservePrice { get; set; } = 0;
    public string Seller { get; set; } = string.Empty;
    public string Winner { get; set; } = string.Empty;
    public int? SoldAmount { get; set; }
    public int? CurrentHightBid { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }  = DateTime.UtcNow;
    public DateTime EndDate { get; set; }  = DateTime.UtcNow;
    public Status Status { get; set; }
    public Item? Item { get; set; }

    public bool HasReservedPrice() => ReservePrice > 0;
}
