using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Repositories.ItemSearchRepository
{
    public class ItemSearchRepository : IItemSearchRepository
    {
        public async Task<SearchList> SearchItems(SearchRequest searchRequest)
        {
            var query = DB.PagedSearch<Item>();
            query.Sort(x => x.Ascending(a => a.CreatedAt));
            if(!string.IsNullOrEmpty(searchRequest.SearchTerm))
            {
                query.Match(Search.Full, searchRequest.SearchTerm).SortByTextScore();
            }  
            query.PageNumber(searchRequest.PageNumber);
            query.PageSize(searchRequest.PageSize);
          
            var result  = await query.ExecuteAsync();
            return new SearchList { 
                Items = result.Results?.ToList(), 
                PageCount =  result.PageCount, 
                TotalCount = result.TotalCount 
                };
        }
    }
}