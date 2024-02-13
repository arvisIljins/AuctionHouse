namespace SearchService.Models
{
    public class SearchList
    {
        public List<Item>? Items { get; set; }
        public int PageCount { get; set; }
        public long TotalCount { get; set; }
    }
}