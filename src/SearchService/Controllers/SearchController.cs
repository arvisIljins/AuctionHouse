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
        public async Task<ActionResult<SearchList>> SearchItem([FromQuery] SearchRequest searchRequest)
        {
            return Ok(await _itemSearchService.SearchItems(searchRequest));
        }
    }
}