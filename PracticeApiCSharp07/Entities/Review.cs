namespace PracticeApiCSharp07.Entities
{
    internal class Review : EntityBase
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public required string ReviewerName { get; set; }
        public required string Comment { get; set; }
        public int Rating { get; set; } // Rating is an integer from 1 to 5
        public Movie Movie { get; set; } = null!;
    }
}
