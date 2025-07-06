using PracticeApiCSharp07.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PracticeApiCSharp07.DTOs.Reviews
{
    public class CreateReviewDTO
    {
        public int MovieId { get; set; }

        [StringLength(256)]
        public required string ReviewerName { get; set; }

        [StringLength(5000)]
        public required string Comment { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
