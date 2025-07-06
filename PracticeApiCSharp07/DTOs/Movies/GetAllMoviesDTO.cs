using PracticeApiCSharp07.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PracticeApiCSharp07.DTOs.Movies
{
    /// <summary>
    /// Represents filter and pagination parameters for retrieving a list of movies.
    /// </summary>
    public class GetAllMoviesDTO
    {
        /// <summary>
        /// The number of movies to skip. Must be zero or greater.
        /// </summary>
        [Range(0, int.MaxValue)]
        public int Skip { get; set; } = 0;

        /// <summary>
        /// The number of movies to take. Must be between 1 and 100.
        /// </summary>
        [Range(1, 100)]
        public int Take { get; set; } = 50;

        /// <summary>
        /// An optional genre to filter the movies by. Max length is 256 characters.
        /// </summary>
        /// <example>Action</example>
        [StringLength(256)]
        public string? Genre { get; set; }

        /// <summary>
        /// An optional release year to filter the movies by. Must be between 1888 and the current year if provided.
        /// </summary>
        /// <example>2023</example>
        [YearUntilNow(1888)]
        public int? Year { get; set; }

        /// <summary>
        /// An optional actor name to filter the movies by. Max length is 256 characters.
        /// </summary>
        /// <example>John Doe</example>
        [StringLength(256)]
        public string? Actor { get; set; }
    }

}
