namespace PracticeApiCSharp07.DTOs.Reviews
{
    /// <summary>
    /// Represents review data returned to the client.
    /// </summary>
    public class ReviewDTO
    {
        /// <summary>
        /// The unique identifier of the review.
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// The name of the person who wrote the review.
        /// </summary>
        /// <example>John Doe</example>
        public required string ReviewerName { get; set; }

        /// <summary>
        /// The textual content of the review.
        /// </summary>
        /// <example>An amazing movie with stunning visuals and a compelling story.</example>
        public required string Comment { get; set; }

        /// <summary>
        /// The numeric rating given by the reviewer.
        /// </summary>
        /// <example>5</example>
        public int Rating { get; set; }
    }
}
