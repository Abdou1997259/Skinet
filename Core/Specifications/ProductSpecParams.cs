namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int maxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        private int pageSize = 6;
        public int PageSize
        {
            get => pageSize;
            set=> pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        private string? _search;
        public string? Search
        {
            get => _search;
            set => _search = value?.ToLower();
        }
        public SortOptions SortOptions { get; set; }
    }
}
