namespace AuctionService.Repositories.AuctionsRepository
{
    public interface IAuctionsRepository
    {
        Task<List<Auction>> GetAuctionsAsync();
        Task<Auction> CreateAuctions(Auction auction);
    }
}