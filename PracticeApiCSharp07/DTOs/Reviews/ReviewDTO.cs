namespace PracticeApiCSharp07.DTOs.Reviews
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public required string ReviewerName { get; set; }
        public required string Comment { get; set; }
        public int Rating { get; set; }
    }
}
