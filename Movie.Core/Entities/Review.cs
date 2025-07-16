namespace Movie.Core.Entities
{
    public class Review : EntityBase
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public required string ReviewerName { get; set; }
        public required string Comment { get; set; }
        public int Rating { get; set; } // Rating is an integer from 1 to 5
        public Film Film { get; set; } = null!;
    }
}
