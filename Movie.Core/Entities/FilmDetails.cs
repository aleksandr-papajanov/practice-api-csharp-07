namespace Movie.Core.Entities
{
    public class FilmDetails : EntityBase
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public required string Synopsis { get; set; }
        public required string Language { get; set; }
        public decimal Budget { get; set; }
        public Film Film { get; set; } = null!;
    }
}
