#pragma warning disable CS1591

namespace Movie.Core.Entities
{
    public class FilmActor : EntityBase
    {
        // Foreign keys
        public int FilmId { get; set; }
        public int ActorId { get; set; }

        // Navigation properties
        public Film Film { get; set; } = null!;
        public Actor Actor { get; set; } = null!;
    }
}
