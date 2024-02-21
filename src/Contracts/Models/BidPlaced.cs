namespace Contracts.Models
{
    public class BidPlaced
    {
        public string Id { get; set; } = string.Empty;
        public string AuctionId { get; set; } = string.Empty;
        public string Bidder { get; set; } = string.Empty;
        public DateTime Time { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}