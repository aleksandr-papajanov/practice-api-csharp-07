using PracticeApiCSharp07.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PracticeApiCSharp07.DTOs.Movies
{
    public class CreateMovieDTO
    {
        [StringLength(256)]
        public required string Title { get; set; }

        [StringLength(256)]
        public required string Genre { get; set; }

        [YearUntilNow(1888)]
        public int Year { get; set; }

        [Range(1, 9999)]
        public int Duration { get; set; }

        [StringLength(5000)]
        public required string Synopsis { get; set; }

        [StringLength(256)]
        public required string Language { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Budget { get; set; }
    }
}
