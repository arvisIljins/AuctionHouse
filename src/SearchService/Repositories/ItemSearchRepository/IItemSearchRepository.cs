using SearchService.Models;

namespace SearchService.Repositories.ItemSearchRepository
{
    public interface IItemSearchRepository
    {
        Task<List<Item>> SearchItems(string searchTerm); 
    }
}