using PracticeApiCSharp07.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PracticeApiCSharp07.DTOs.Movies
{
    /// <summary>
    /// Represents the data required to create a new movie.
    /// </summary>
    public class CreateMovieDTO
    {
        /// <summary>
        /// The title of the movie. Required. Max length is 256 characters.
        /// </summary>
        /// <example>Inception</example>
        [StringLength(256)]
        public required string Title { get; set; }

        /// <summary>
        /// The genre of the movie. Required. Max length is 256 characters.
        /// </summary>
        /// <example>Science Fiction</example>
        [StringLength(256)]
        public required string Genre { get; set; }

        /// <summary>
        /// The release year of the movie. Must be between 1888 and the current year.
        /// </summary>
        /// <example>2010</example>
        [YearUntilNow(1888)]
        public int Year { get; set; }

        /// <summary>
        /// The duration of the movie in minutes. Must be between 1 and 9999.
        /// </summary>
        /// <example>148</example>
        [Range(1, 9999)]
        public int Duration { get; set; }

        /// <summary>
        /// A short summary of the movie. Required. Max length is 5000 characters.
        /// </summary>
        /// <example>A mind-bending thriller that explores the world of dreams and reality.</example>
        [StringLength(5000)]
        public required string Synopsis { get; set; }

        /// <summary>
        /// The language of the movie. Required. Max length is 256 characters.
        /// </summary>
        /// <example>English</example>
        [StringLength(256)]
        public required string Language { get; set; }

        /// <summary>
        /// The budget of the movie in currency units. Must be greater than 0.
        /// </summary>
        /// <example>160000000.00</example>
        [Range(0.01, double.MaxValue)]
        public decimal Budget { get; set; }
    }

}
