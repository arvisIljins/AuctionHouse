using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.Service.SearchItemService;

namespace SearchService.Data
{
    public class DBInitializer
    {
        public static async Task InitDb(WebApplication app)
        {
            await DB.InitAsync("SearchDb", MongoClientSettings.FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

            await DB.Index<Item>()
            .Key(x => x.Title, KeyType.Text)
            .Key(x => x.Description, KeyType.Text)
            .Key(x => x.Tags, KeyType.Text)
            .CreateAsync();

            var count = await DB.CountAsync<Item>();
            
            using var scope = app.Services.CreateScope();
            var httpClient = scope.ServiceProvider.GetRequiredService<AuctionServiceHttpClient>();
            var items = await httpClient.GetItemsForSearchDb();
            
            if(items.Count > 0) {
                await DB.SaveAsync(items);
            }
        }
    }
}