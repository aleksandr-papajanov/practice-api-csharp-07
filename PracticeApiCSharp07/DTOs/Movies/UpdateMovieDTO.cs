using PracticeApiCSharp07.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PracticeApiCSharp07.DTOs.Movies
{
    public class UpdateMovieDTO
    {
        [StringLength(256)]
        public string? Title { get; set; }

        [StringLength(256)]
        public string? Genre { get; set; }

        [YearUntilNow(1888)]
        public int? Year { get; set; }

        [Range(1, 9999)]
        public int? Duration { get; set; }

        [StringLength(5000)]
        public string? Synopsis { get; set; }

        [StringLength(256)]
        public string? Language { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal? Budget { get; set; }
    }
}
