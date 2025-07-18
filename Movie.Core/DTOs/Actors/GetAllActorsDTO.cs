using System.ComponentModel.DataAnnotations;

namespace Movie.Core.DTOs.Actors
{
    /// <summary>
    /// Represents pagination parameters for retrieving a list of actors.
    /// </summary>
    public class GetAllActorsDTO
    {
        /// <summary>
        /// The page number to retrieve. Must be 1 or greater.
        /// </summary>
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// The number of actors per page. Must be between 1 and 100.
        /// </summary>
        [Range(1, 100)]
        public int PageSize { get; set; } = 20;
    }
}
