using System.ComponentModel.DataAnnotations;

namespace PracticeApiCSharp07.DTOs.Reviews
{
    public class UpdateReviewDTO
    {
        [StringLength(256)]
        public string? ReviewerName { get; set; }

        [StringLength(5000)]
        public string? Comment { get; set; }

        [Range(1, 5 )]
        public int? Rating { get; set; } 
    }
}
