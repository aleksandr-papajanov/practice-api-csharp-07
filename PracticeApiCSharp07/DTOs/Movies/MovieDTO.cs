namespace PracticeApiCSharp07.DTOs.Movies
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Genre { get; set; }
        public int Year { get; set; }
        public int Duration { get; set; }
    }
}
