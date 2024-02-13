using SearchService.Models;

namespace SearchService.Repositories.ItemSearchRepository
{
    public interface IItemSearchRepository
    {
        Task<SearchList> SearchItems(SearchRequest searchRequest); 
    }
}