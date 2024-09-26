namespace Catalog.Core.Specs
{
    public class Pagination<T> where T : class
    {
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int PageIndex { get; set; }
        IReadOnlyList<T> Data { get; set; }
        public Pagination() { }
        public Pagination(int pageSize, int pageIndex, int count, IReadOnlyList<T> data)
        {
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.PageCount = count;
            this.Data = data;
        }
    }
}
