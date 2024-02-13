using SearchService.Models;

namespace SearchService.Service.SearchItemService
{
    public interface IItemSearchService
    {
          Task<ServiceResponse<List<Item>>> SearchItems(string searchTerm);  
    }
}