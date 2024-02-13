using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Repositories.ItemSearchRepository
{
    public class ItemSearchRepository : IItemSearchRepository
    {
        public async Task<SearchList> SearchItems(SearchRequest searchRequest)
        {
            var query = DB.PagedSearch<Item, Item>();
            if(!string.IsNullOrEmpty(searchRequest.SearchTerm))
            {
                query.Match(Search.Full, searchRequest.SearchTerm).SortByTextScore();
            }  
            query.PageNumber(searchRequest.PageNumber);
            query.PageSize(searchRequest.PageSize);

            query = searchRequest.OrderBy switch
            {
                "createdAt" => query.Sort(x => x.Ascending(a => a.CreatedAt)),
                "updatedAt" => query.Sort(x => x.Ascending(a => a.UpdatedAt)),
                "reservePrice" => query.Sort(x => x.Ascending(a => a.ReservePrice)),
                _ => query.Sort(x => x.Ascending(a => a.EndDate))
            };

             query = searchRequest.FilterBy switch
            {
                "finished" => query.Match(x => x.EndDate < DateTime.UtcNow),
                "endingSoon" => query.Match(x => x.EndDate < DateTime.UtcNow.AddHours(6) 
                    && x.EndDate > DateTime.UtcNow),
                _ => query.Match(x => x.EndDate > DateTime.UtcNow),
            };

            if(!string.IsNullOrEmpty(searchRequest.Seller)) {
                 query.Match(x => x.Seller == searchRequest.Seller);
            }

            if(!string.IsNullOrEmpty(searchRequest.Winner)) {
                 query.Match(x => x.Winner == searchRequest.Winner);
            }
 
            var result  = await query.ExecuteAsync();
            return new SearchList { 
                Items = result.Results?.ToList(), 
                PageCount =  result.PageCount, 
                TotalCount = result.TotalCount 
                };
        }
    }
}