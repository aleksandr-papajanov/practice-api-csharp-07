namespace Movie.Core.DTOs.Common
{
    /// <summary>
    /// Represents a paginated result for a collection of items.
    /// </summary>
    /// <typeparam name="T">The type of items in the paginated result.</typeparam>
    public class PaginatedResult<T>
    {
        /// <summary>
        /// The collection of items for the current page.
        /// </summary>
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();

        /// <summary>
        /// The total number of items across all pages.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// The size of each page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The current page number (1-based index).
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// The total number of pages.
        /// </summary>
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        /// <summary>
        /// Indicates whether there is a next page.
        /// </summary>
        public bool HasNextPage => CurrentPage < TotalPages;

        /// <summary>
        /// Indicates whether there is a previous page.
        /// </summary>
        public bool HasPreviousPage => CurrentPage > 1;

        /// <summary>
        /// Creates a paginated result.
        /// </summary>
        /// <param name="items">The collection of items for the current page.</param>
        /// <param name="totalCount">The total number of items across all pages.</param>
        /// <param name="pageSize">The size of each page.</param>
        /// <param name="currentPage">The current page number (1-based index).</param>
        public PaginatedResult(IEnumerable<T> items, int totalCount, int pageSize, int currentPage)
        {
            Items = items;
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
        }
    }
}