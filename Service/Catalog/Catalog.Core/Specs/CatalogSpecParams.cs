namespace Catalog.Core.Specs
{
    public class CatalogSpecParams
    {
        private const int _maxPageSize = 70;
        private int _pageSize;
        public int PageIndex { get; set; } = 1;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value > 0)
                {
                    _pageSize = value;
                }
            }
        }
        public string? BrandId { get; set; }
        public string? TypeId { get; set; }
        public string? Sort { get; set; }
        public string? Search { get; set; }
    }
}
