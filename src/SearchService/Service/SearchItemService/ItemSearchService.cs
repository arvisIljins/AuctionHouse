using SearchService.Models;
using SearchService.Repositories.ItemSearchRepository;

namespace SearchService.Service.SearchItemService
{
    public class ItemSearchService : IItemSearchService
    {
        private readonly IItemSearchRepository _itemSearchRepository;

        public ItemSearchService(IItemSearchRepository itemSearchRepository)
        {
            _itemSearchRepository = itemSearchRepository;
        }

        public async Task<ServiceResponse<SearchList>> SearchItems(SearchRequest searchRequest)
        {
            var serviceResponse = new ServiceResponse<SearchList>();
            try
            {   
                var items = await _itemSearchRepository.SearchItems(searchRequest);
                serviceResponse.Data = items;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Error retrieving auctions. Error - {ex}";
            }
            return serviceResponse;
        }
    }
}