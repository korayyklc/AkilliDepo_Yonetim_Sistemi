namespace AkilliDepo.API.DTOs
{
    // Test dokümanındaki sayfalama kuralına uygun dönüş modeli
    public class PagedResult<T>
    {
        public bool Success { get; set; } = true;
        public IEnumerable<T> Data { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}