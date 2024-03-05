using AuctionService.Data;

namespace AuctionService.IntegrationTests.Util
{
    public static class DbHelper
    {
        public static void InitDbForTests(AuctionDbContext dbContext)
        {
            dbContext.Auctions.AddRange(GetAuctionsForTests());
            dbContext.SaveChanges();
        }

        public static void ReInitDbForTests(AuctionDbContext dbContext)
        {
            dbContext.Auctions.RemoveRange(dbContext.Auctions);
            dbContext.SaveChanges();
            InitDbForTests(dbContext);
        }

        private static List<Auction> GetAuctionsForTests()
        {
            return new List<Auction> 
            {
                new Auction
                {
                    Id = Guid.Parse("470fb15b-9371-4011-8bdb-c5b652edc205"),
                    ReservePrice = 1000,
                    Seller = "Arvis",
                    Winner = "",
                    SoldAmount = 0,
                    CurrentHightBid = 0,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(10),
                    Status = Status.Live,
                    Item = new Item 
                    {
                        ImageUrl = "www.image1-test.com",
                        Title = "House 1",
                        Description = "House 1 description",
                        Tags = "house, 1, test"
                    }
                },
                new Auction
                {
                    Id = Guid.Parse("75cdf40e-b769-4870-9b47-8e965cd428b6"),
                    ReservePrice = 2000,
                    Seller = "Arvis",
                    Winner = "",
                    SoldAmount = 0,
                    CurrentHightBid = 0,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(10),
                    Status = Status.Live,
                    Item = new Item 
                    {
                        ImageUrl = "www.image2-test.com",
                        Title = "House 2",
                        Description = "House 2 description",
                        Tags = "house, 2, test"
                    }
                },
                new Auction
                {
                    Id = Guid.Parse("91e7b86b-9aa7-49a3-89d0-0c9d1ad3c98f"),
                    ReservePrice = 3000,
                    Seller = "Arvis",
                    Winner = "",
                    SoldAmount = 0,
                    CurrentHightBid = 0,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(10),
                    Status = Status.Live,
                    Item = new Item 
                    {
                        ImageUrl = "www.image2-test.com",
                        Title = "House 2",
                        Description = "House 2 description",
                        Tags = "house, 2, test"
                    }
                }
            };
        }
        
    }
}