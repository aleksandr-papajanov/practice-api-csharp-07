namespace PracticeApiCSharp07.Entities
{
    internal class MovieDetails : EntityBase
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public required string Synopsis { get; set; }
        public required string Language { get; set; }
        public decimal Budget { get; set; }
        public Movie Movie { get; set; } = null!;
    }
}
