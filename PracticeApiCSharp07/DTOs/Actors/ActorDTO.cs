namespace PracticeApiCSharp07.DTOs.Actors
{
    public class ActorDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int BirthYear { get; set; }
        public IEnumerable<string> Movies { get; set; } = [];
    }
}
