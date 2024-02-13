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

        public async Task<ServiceResponse<List<Item>>> SearchItems(string searchTerm)
        {
            var serviceResponse = new ServiceResponse<List<Item>>();
            try
            {
                var items = await _itemSearchRepository.SearchItems(searchTerm);
                if(items is null){
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No items found";
                }


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