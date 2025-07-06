namespace PracticeApiCSharp07.DTOs.Movies
{
    /// <summary>
    /// Represents movie data returned to the client.
    /// </summary>
    public class MovieDTO
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
        /// The year the movie was released.
        /// </summary>
        /// <example>1999</example>
        public int Year { get; set; }

        /// <summary>
        /// The duration of the movie in minutes.
        /// </summary>
        /// <example>136</example>
        public int Duration { get; set; }
    }
}
