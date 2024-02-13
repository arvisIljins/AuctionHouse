using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Repositories.ItemSearchRepository
{
    public class ItemSearchRepository : IItemSearchRepository
    {
        public async Task<List<Item>> SearchItems(string searchTerm)
        {
            var query = DB.Find<Item>();
            var result  = await query.ExecuteAsync();
            return result;
        }
    }
}