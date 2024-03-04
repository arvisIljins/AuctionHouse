using AutoFixture;
using Contracts.Models;
using Moq;
using SearchService.Models;
using SearchService.Repositories.ItemSearchRepository;
using SearchServiceClass = SearchService.Service.SearchItemService.ItemSearchService;

namespace SearchService.UnitTests;

public class SearchServiceTests
{
    private readonly Mock<IItemSearchRepository> _searchRepository = new Mock<IItemSearchRepository>();
    private readonly Fixture _fixture = new Fixture();
    private readonly SearchServiceClass _searchService;

    public SearchServiceTests()
    {
        _searchService = new SearchServiceClass(
        _searchRepository.Object
        );
    }
    [Fact]
    public void SearchItems_WithNoParams_ReturnAuctions()
    {
        var auctions = _fixture.CreateMany<Item>(10).ToList();
        var response  = new SearchList {
            Items = auctions,
            PageCount = 5,
            TotalCount = 25
        };
        var searchRequest =  new SearchRequest();
        _searchRepository.Setup(repo => repo.SearchItems(searchRequest)).ReturnsAsync(response);

        //act
        var result = _searchService.SearchItems(searchRequest);

        // assert
        Assert.NotNull(result.Result.Data?.Items);
        Assert.Equal(10, result?.Result.Data?.Items.Count);
        Assert.True(result?.Result.Success);
        Assert.IsType<ServiceResponse<SearchList>>(result?.Result);
    }

    [Fact]
    public void SearchItems_DatabaseError_ReturnSuccessFalse()
    {
        var auctions = _fixture.CreateMany<Item>(10).ToList();
        var response  = new SearchList {
            Items = auctions,
            PageCount = 5,
            TotalCount = 25
        };
        var searchRequest =  new SearchRequest();
        var exceptionMessage = "Db down";
        _searchRepository.Setup(repo => repo.SearchItems(searchRequest)).Throws(new Exception(exceptionMessage));

        //act
        var result = _searchService.SearchItems(searchRequest);

        // assert
        Assert.False(result?.Result.Success);
        Assert.True(result?.Result.Data is null);
        Assert.IsType<ServiceResponse<SearchList>>(result?.Result);
    }
}