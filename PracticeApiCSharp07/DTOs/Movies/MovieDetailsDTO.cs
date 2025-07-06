using PracticeApiCSharp07.DTOs.Reviews;

namespace PracticeApiCSharp07.DTOs.Movies
{
    /// <summary>
    /// Represents detailed information about a movie, including its actors and reviews.
    /// </summary>
    public class MovieDetailsDTO
    {
        /// <summary>
        /// The unique identifier of the movie.
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// The title of the movie.
        /// </summary>
        /// <example>The Matrix</example>
        public required string Title { get; set; }

        /// <summary>
        /// The genre of the movie.
        /// </summary>
        /// <example>Science Fiction</example>
        public required string Genre { get; set; }

        /// <summary>
        /// The release year of the movie.
        /// </summary>
        /// <example>1999</example>
        public int Year { get; set; }

        /// <summary>
        /// The duration of the movie in minutes.
        /// </summary>
        /// <example>136</example>
        public int Duration { get; set; }

        /// <summary>
        /// A short summary or synopsis of the movie.
        /// </summary>
        /// <example>A computer hacker learns about the true nature of his reality and his role in the war against its controllers.</example>
        public required string Synopsis { get; set; }

        /// <summary>
        /// The language of the movie.
        /// </summary>
        /// <example>English</example>
        public required string Language { get; set; }

        /// <summary>
        /// The budget of the movie.
        /// </summary>
        /// <example>63000000.00</example>
        public decimal Budget { get; set; }

        /// <summary>
        /// A collection of actor names who starred in the movie.
        /// </summary>
        /// <example>["Keanu Reeves", "Laurence Fishburne", "Carrie-Anne Moss"]</example>
        public IEnumerable<string> Actors { get; set; } = [];

        /// <summary>
        /// A collection of reviews for the movie.
        /// </summary>
        /// <example>
        /// [
        ///     {
        ///         "Id": 1,
        ///         "ReviewerName": "John Doe",
        ///         "Comment": "An amazing movie with stunning visuals and a compelling story.",
        ///         "Rating": 5
        ///     },
        ///     {
        ///         "Id": 2,
        ///         "ReviewerName": "Jane Smith",
        ///         "Comment": "A thought-provoking masterpiece that redefines the genre.",
        ///         "Rating": 4
        ///     }
        /// ]
        /// </example>
        public IEnumerable<ReviewDTO> Reviews { get; set; } = [];
    }

}
