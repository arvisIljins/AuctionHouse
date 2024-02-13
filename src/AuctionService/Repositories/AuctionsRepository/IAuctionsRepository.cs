namespace AuctionService.Repositories.AuctionsRepository
{
    public interface IAuctionsRepository
    {
        Task<List<AuctionDto>> GetAuctionsAsync(string? date);
        Task<Auction> CreateAuction(Auction auction);
        Task<Auction> UpdateAuction(UpdateAuctionDto auction); 
        Task<bool> DeleteAuction(Guid id); 
    }
}