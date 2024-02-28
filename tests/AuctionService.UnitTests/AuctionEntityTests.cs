namespace AuctionService.UnitTests;

public class AuctionEntityTests
{
    [Fact]
    public void HasReservedPrice_ReservePriceGreaterThenZero_True()
    {
        // arrange
        var auction = new Auction{Id = Guid.NewGuid(), ReservePrice = 10};

        //act

        var result = auction.HasReservedPrice();

        // assert
        Assert.True(result);

    }

        [Fact]
    public void HasReservedPrice_ReservePriceIsZero_False()
    {
        // arrange
        var auction = new Auction{Id = Guid.NewGuid(), ReservePrice = 0};

        //act

        var result = auction.HasReservedPrice();

        // assert
        Assert.False(result);

    }
}