using PracticeApiCSharp07.DTOs.Reviews;

namespace PracticeApiCSharp07.DTOs.Movies
{
    public class MovieDetailsDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Genre { get; set; }
        public int Year { get; set; }
        public int Duration { get; set; }
        public required string Synopsis { get; set; }
        public required string Language { get; set; }
        public decimal Budget { get; set; }
        public IEnumerable<string> Actors { get; set; } = [];
        public IEnumerable<ReviewDTO> Reviews { get; set; } = [];

    }
}
