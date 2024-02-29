namespace AuctionService.Repositories.AuctionsRepository
{
    public interface IAuctionsRepository
    {
        Task<List<AuctionDto>> GetAuctionsAsync(string? date);
        Task<AuctionDto> GetAuctionsByIdAsync(Guid id);
        Task<Auction> GetAuctionsEntityByIdAsync(Guid id);
        void CreateAuction(Auction auction);
        Task<bool> SaveChangesAsync(); 
        void DeleteAuction(Auction auction); 
    }
}