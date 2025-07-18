#pragma warning disable CS1591

namespace Movie.Core.Entities
{
    public class FilmDetails : EntityBase
    {
        public int Id { get; set; }
        public required string Synopsis { get; set; }
        public required string Language { get; set; }
        public decimal Budget { get; set; }

        // Foreign key
        public int FilmId { get; set; }

        // Navigation property
        public Film Film { get; set; } = null!;
    }
}
