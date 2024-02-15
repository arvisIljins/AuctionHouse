namespace SearchService.Models
{
    public class SearchRequest
    {
        public string SearchTerm { get; set; } = string.Empty;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 4;
        public string Seller { get; set; } = string.Empty;
        public string Winner { get; set; } = string.Empty;
        public string OrderBy { get; set; } = "createdAt";
        public string FilterBy { get; set; } = string.Empty;
    }
}