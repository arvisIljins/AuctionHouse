using Contracts.Models;
using SearchService.Models;

namespace SearchService.Service.SearchItemService
{
    public interface IItemSearchService
    {
          Task<ServiceResponse<SearchList>> SearchItems(SearchRequest searchRequest);  
    }
}