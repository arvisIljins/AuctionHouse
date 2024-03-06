using System.Net;
using System.Net.Http.Json;
using AuctionService.Data;
using AuctionService.IntegrationTests.Fixtures;
using AuctionService.IntegrationTests.Util;
using Contracts.Models;
using Microsoft.Extensions.DependencyInjection;

// Testing controller
namespace AuctionService.IntegrationTests
{
    public class AuctionControllerTests : IClassFixture<CustomWebAppFactory>, IAsyncLifetime
    {
        private readonly CustomWebAppFactory _customWebAppFactory;
        private readonly HttpClient _httpClient;
        const string auctionId = "470fb15b-9371-4011-8bdb-c5b652edc205";
        const string wrongId =  "c87b594f-338c-4d32-bc50-0d30e2973643";

        public AuctionControllerTests(CustomWebAppFactory customWebAppFactory)
        {
            _customWebAppFactory = customWebAppFactory;
            _httpClient = customWebAppFactory.CreateClient();
        }

        public Task InitializeAsync() => Task.CompletedTask;
        public Task DisposeAsync()
        {
            using var scope = _customWebAppFactory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AuctionDbContext>();
            DbHelper.ReInitDbForTests(db);
            return Task.CompletedTask;
        }

        private CreateAuctionDto GetCreateAuctionDto()
        {
            return new CreateAuctionDto
            {
                ImageUrl = "image",
                Title = "tests title",
                Description = "test description",
                Tags = "test tags",
                ReservePrice = 1000,
                EndDate = DateTime.UtcNow.AddDays(10)
            };
        }

        private UpdateAuctionDto GetUpdateAuctionDto()
        {
            return new UpdateAuctionDto
            {
                Id = Guid.Parse("470fb15b-9371-4011-8bdb-c5b652edc205"),
                ImageUrl = "image",
                Title = "tests title update",
                Description = "test description update",
                Tags = "test tags update",
                ReservePrice = 2000,
                EndDate = DateTime.UtcNow.AddDays(10)
            };
        }

        [Fact]
        public async void GetAuctions_Return3Auctions()
        {
            // arrange

            //act
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<AuctionDto>>>("api/auctions");

            // assert
            Assert.Equal(3, response?.Data?.Count);
            Assert.True(response?.Success);
            Assert.True(response?.Data is not null);
            Assert.IsType<ServiceResponse<List<AuctionDto>>>(response);
        }

        [Fact]
        public async void CreateAuctions_WithNoAuth_Return401()
        {
            // arrange
            var auction = new CreateAuctionDto{ Title = "Test"};

            //act
            var response = await _httpClient.PostAsJsonAsync("api/auctions", auction);

            // assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void CreateAuctions_WithAuth_ReturnSuccess()
        {
            // arrange
            var auction = GetCreateAuctionDto();
            _httpClient.SetFakeJwtBearerToken(AuthenticationHelper.GetBearerForUser("Arvis"));

            //act
            var response = await _httpClient.PostAsJsonAsync("api/auctions", auction);

            // assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var createAuctionResult = await response.Content.ReadFromJsonAsync<ServiceResponse<AuctionDto>>();
            Assert.NotNull(createAuctionResult);
            Assert.True(createAuctionResult.Success);
            Assert.NotNull(createAuctionResult.Data);
            Assert.Equal("Arvis", createAuctionResult.Data.Seller);
            Assert.IsType<ServiceResponse<AuctionDto>>(createAuctionResult);
        }

        [Fact]
        public async void UpdateAuctions_WithAuth_ReturnSuccess()
        {
            // arrange
            var auction = GetUpdateAuctionDto();
            _httpClient.SetFakeJwtBearerToken(AuthenticationHelper.GetBearerForUser("Arvis"));

            //act
            var response = await _httpClient.PutAsJsonAsync("api/auctions", auction);

            // assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var updatedAuctionResult = await response.Content.ReadFromJsonAsync<ServiceResponse<AuctionDto>>();
            Assert.NotNull(updatedAuctionResult);
            Assert.True(updatedAuctionResult.Success);
            Assert.NotNull(updatedAuctionResult.Data);
            Assert.IsType<ServiceResponse<AuctionDto>>(updatedAuctionResult);
        }

        [Fact]
        public async void UpdateAuctions_WithWrongAuth_ReturnSuccessFalse()
        {
            // arrange
            var auction = GetUpdateAuctionDto();
            _httpClient.SetFakeJwtBearerToken(AuthenticationHelper.GetBearerForUser("Arvis-fake"));

            //act
            var response = await _httpClient.PutAsJsonAsync("api/auctions", auction);

            // assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var updatedAuctionResult = await response.Content.ReadFromJsonAsync<ServiceResponse<AuctionDto>>();
            Assert.NotNull(updatedAuctionResult);
            Assert.False(updatedAuctionResult.Success);
            Assert.True(updatedAuctionResult.Data == null);
            Assert.IsType<ServiceResponse<AuctionDto>>(updatedAuctionResult);
        }

        [Fact]
        public async void UpdateAuctions_WithWrongId_ReturnSuccessFalse()
        {
            // arrange
            var auction = GetUpdateAuctionDto();
            auction.Id = Guid.Parse(wrongId);
            _httpClient.SetFakeJwtBearerToken(AuthenticationHelper.GetBearerForUser("Arvis"));

            //act
            var response = await _httpClient.PutAsJsonAsync("api/auctions", auction);

            // assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var updatedAuctionResult = await response.Content.ReadFromJsonAsync<ServiceResponse<AuctionDto>>();
            Assert.NotNull(updatedAuctionResult);
            Assert.False(updatedAuctionResult.Success);
            Assert.True(updatedAuctionResult.Data == null);
            Assert.IsType<ServiceResponse<AuctionDto>>(updatedAuctionResult);
        }

        [Fact]
        public async void DeleteAuctions_CorrectIdAndUser_ReturnSuccess()
        {
            // arrange
            _httpClient.SetFakeJwtBearerToken(AuthenticationHelper.GetBearerForUser("Arvis"));

            //act
            var response = await _httpClient.DeleteAsync($"api/auctions/{auctionId}");

            // assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var deletedAuctionResult = await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
            Assert.NotNull(deletedAuctionResult);
            Assert.True(deletedAuctionResult.Success);
            Assert.IsType<ServiceResponse<string>>(deletedAuctionResult);
        }

        [Fact]
        public async void DeleteAuctions_WithWrongUser_ReturnSuccess()
        {
            // arrange
            _httpClient.SetFakeJwtBearerToken(AuthenticationHelper.GetBearerForUser("Arvis-fake"));

            //act
            var response = await _httpClient.DeleteAsync($"api/auctions/{auctionId}");

            // assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var deletedAuctionResult = await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
            Assert.NotNull(deletedAuctionResult);
            Assert.False(deletedAuctionResult.Success);
            Assert.IsType<ServiceResponse<string>>(deletedAuctionResult);
        }

        [Fact]
        public async void DeleteAuctions_WithWrongId_ReturnSuccess()
        {
            // arrange
            _httpClient.SetFakeJwtBearerToken(AuthenticationHelper.GetBearerForUser("Arvis"));

            //act
            var response = await _httpClient.DeleteAsync($"api/auctions/{wrongId}");

            // assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var deletedAuctionResult = await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
            Assert.NotNull(deletedAuctionResult);
            Assert.False(deletedAuctionResult.Success);
            Assert.IsType<ServiceResponse<string>>(deletedAuctionResult);
        }
    }
}