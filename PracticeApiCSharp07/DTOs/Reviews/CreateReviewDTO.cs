using PracticeApiCSharp07.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PracticeApiCSharp07.DTOs.Reviews
{
    /// <summary>
    /// Represents the data required to create a new review for a movie.
    /// </summary>
    public class CreateReviewDTO
    {
        /// <summary>
        /// The identifier of the movie being reviewed.
        /// </summary>
        /// <example>56</example>
        public int MovieId { get; set; }

        /// <summary>
        /// The name of the reviewer. Required. Max length is 256 characters.
        /// </summary>
        /// <example>John Doe</example>
        [StringLength(256)]
        public required string ReviewerName { get; set; }

        /// <summary>
        /// The textual comment of the review. Required. Max length is 5000 characters.
        /// </summary>
        /// <example>This movie was fantastic! I loved the action scenes and the plot twists.</example>
        [StringLength(5000)]
        public required string Comment { get; set; }

        /// <summary>
        /// The rating given by the reviewer. Must be between 1 and 5.
        /// </summary>
        /// <example>5</example>
        [Range(1, 5)]
        public int Rating { get; set; }
    }

}
