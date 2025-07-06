using System.ComponentModel.DataAnnotations;

namespace PracticeApiCSharp07.DTOs.Reviews
{
    /// <summary>
    /// Represents the data used to update an existing review.
    /// Only non-null properties will be updated.
    /// </summary>
    public class UpdateReviewDTO
    {
        /// <summary>
        /// The new name of the reviewer. Optional. Max length is 256 characters.
        /// </summary>
        /// <example>Jane Doe</example>
        [StringLength(256)]
        public string? ReviewerName { get; set; }

        /// <summary>
        /// The new comment text of the review. Optional. Max length is 5000 characters.
        /// </summary>
        /// <example>An updated review comment with more detailed feedback.</example>
        [StringLength(5000)]
        public string? Comment { get; set; }

        /// <summary>
        /// The new rating given by the reviewer. Optional. Must be between 1 and 5.
        /// </summary>
        /// <example>4</example>
        [Range(1, 5)]
        public int? Rating { get; set; }
    }
}
