using AuctionService.Repositories.AuctionsRepository;
using AuctionService.Services.IdentityService;
using AutoFixture;
using AutoMapper;
using Contracts.Models;
using MassTransit;
using Moq;
using AuctionServiceClass = AuctionService.Services.AuctionService.AuctionService;


namespace AuctionService.UnitTests;

    public class AuctionServiceTests
    {
        private readonly Mock<IAuctionsRepository> _auctionRepository = new Mock<IAuctionsRepository>();
        private readonly Mock<IPublishEndpoint> _publishEndpoint = new Mock<IPublishEndpoint>();
        private readonly Mock<IIdentityService> _mockIdentityService = new Mock<IIdentityService>();
        private readonly Fixture _fixture = new Fixture();
        private readonly IMapper _mapper;
        private readonly AuctionServiceClass _auctionService;
    public AuctionServiceTests()
    {
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfiles>();
        });
        _mapper = mapperConfiguration.CreateMapper();
        _mockIdentityService.Setup(x => x.GetUserName()).Returns("TestUser");
        _auctionService = new AuctionServiceClass(
            _mapper,
            _auctionRepository.Object,
            _publishEndpoint.Object,
            _mockIdentityService.Object
        );
    }

    [Fact]
    public async void GetAllAuctions_WithNoParams_ReturnAuctions()
    {
        // arrange
        var auctions = _fixture.CreateMany<AuctionDto>(10).ToList();
        _auctionRepository.Setup(repo => repo.GetAuctionsAsync(null)).ReturnsAsync(auctions);

        //act
        var result = await _auctionService.GetAllAuctions(null);

        // assert
        Assert.NotNull(result.Data);
        Assert.Equal(10, result?.Data?.Count);
        Assert.True(result?.Success);
        Assert.IsType<ServiceResponse<List<AuctionDto>>>(result);
    }

    [Fact]
    public async void CreateAuction_WithValidAuctionDto_ReturnAuctionsSuccess()
    {
        // arrange
        var auction = _fixture.Create<CreateAuctionDto>();
        _auctionRepository.Setup(repo => repo.CreateAuction(It.IsAny<Auction>()));
        _auctionRepository.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(true);

        //act
        var result = await _auctionService.CreateAuction(auction);

        // assert
        Assert.NotNull(result.Data);
        Assert.True(result?.Success);
        Assert.IsType<ServiceResponse<AuctionDto>>(result);
    }

}