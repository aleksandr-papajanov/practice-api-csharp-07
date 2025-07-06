using PracticeApiCSharp07.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PracticeApiCSharp07.DTOs.Movies
{
    public class GetAllMoviesDTO
    {
        [Range(0, int.MaxValue)]
        public int Skip { get; set; } = 0;

        [Range(1, 100)]
        public int Take { get; set; } = 50;

        [StringLength(256)]
        public string? Genre { get; set; }
        
        [YearUntilNow(1888)]
        public int? Year { get; set; }

        [StringLength(256)]
        public string? Actor { get; set; }
    }
}
