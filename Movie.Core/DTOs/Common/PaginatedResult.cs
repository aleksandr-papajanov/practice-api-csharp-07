namespace Movie.Core.DTOs.Common
{
    /// <summary>
    /// Represents a paginated result for a collection of items.
    /// </summary>
    /// <typeparam name="T">The type of items in the paginated result.</typeparam>
    public class PaginatedResult<T>(IEnumerable<T> items, int totalCount, int pageSize, int currentPage)
    {
        /// <summary>
        /// The collection of items for the current page.
        /// </summary>
        public IEnumerable<T> Items { get; set; } = items;

        /// <summary>
        /// The total number of items across all pages.
        /// </summary>
        public int TotalCount { get; set; } = totalCount;

        /// <summary>
        /// The size of each page.
        /// </summary>
        public int PageSize { get; set; } = pageSize;

        /// <summary>
        /// The current page number (1-based index).
        /// </summary>
        public int CurrentPage { get; set; } = currentPage;

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
    }
}