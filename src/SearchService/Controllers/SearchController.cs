using Microsoft.AspNetCore.Mvc;
using SearchService.Models;
using SearchService.Service.SearchItemService;

namespace SearchService.Controllers
{
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly IItemSearchService _itemSearchService;

        public SearchController(IItemSearchService itemSearchService)
        {
            _itemSearchService = itemSearchService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Item>>> SearchItem(string searchTerm)
        {
            return Ok(await _itemSearchService.SearchItems(searchTerm));
        }
    }
}