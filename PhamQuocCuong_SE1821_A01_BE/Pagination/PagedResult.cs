namespace PhamQuocCuong_SE1821_A01_BE.Pagination
{
    public class PagedResult<T>
    {
        public IEnumerable<T>? Items { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasNextPage { get => PageIndex < TotalPages; }
        public bool HasPreviousPage { get => PageIndex > 1; }

        public PagedResult(IEnumerable<T> items, int pageIndex, int pageSize, int totalCount)
        {
            Items = items;
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalCount = totalCount;
        }
    }
}
