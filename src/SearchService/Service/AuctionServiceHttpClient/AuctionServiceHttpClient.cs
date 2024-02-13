using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Service.SearchItemService
{
    public class AuctionServiceHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public AuctionServiceHttpClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<Item>> GetItemsForSearchDb()
        {
            var lastUpdate = await DB.Find<Item, string>()
            .Sort(x => x.Descending(x => x.UpdatedAt))
            .Project(x => x.UpdatedAt.ToString())
            .ExecuteFirstAsync();

            var result = await _httpClient.GetFromJsonAsync<ApiResponse>(_config["AuctionServiceUrl"] + "/api/auctions?date=" + lastUpdate);
            return result?.Data ?? new List<Item>{};
        }
    }
}