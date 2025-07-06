using PracticeApiCSharp07.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PracticeApiCSharp07.DTOs.Movies
{
    /// <summary>
    /// Represents the data used to update an existing movie.
    /// Only non-null properties will be updated.
    /// </summary>
    public class UpdateMovieDTO
    {
        /// <summary>
        /// The new title of the movie. Optional. Max length is 256 characters.
        /// </summary>
        /// <example>Inception</example>
        [StringLength(256)]
        public string? Title { get; set; }

        /// <summary>
        /// The new genre of the movie. Optional. Max length is 256 characters.
        /// </summary>
        /// <example>Comedy</example>
        [StringLength(256)]
        public string? Genre { get; set; }

        /// <summary>
        /// The new release year of the movie. Optional. Must be between 1888 and the current year.
        /// </summary>
        /// <example>2023</example>
        [YearUntilNow(1888)]
        public int? Year { get; set; }

        /// <summary>
        /// The new duration of the movie in minutes. Optional. Must be between 1 and 9999.
        /// </summary>
        /// <example>120</example>
        [Range(1, 9999)]
        public int? Duration { get; set; }

        /// <summary>
        /// The new synopsis of the movie. Optional. Max length is 5000 characters.
        /// </summary>
        /// <example>A mind-bending thriller that explores the nature of dreams and reality.</example>
        [StringLength(5000)]
        public string? Synopsis { get; set; }

        /// <summary>
        /// The new language of the movie. Optional. Max length is 256 characters.
        /// </summary>
        /// <example>e.g. "English", "Spanish"</example>
        [StringLength(256)]
        public string? Language { get; set; }

        /// <summary>
        /// The new budget of the movie. Optional. Must be greater than 0.
        /// </summary>
        /// <example>100000000.00</example>
        [Range(0.01, double.MaxValue)]
        public decimal? Budget { get; set; }
    }

}
