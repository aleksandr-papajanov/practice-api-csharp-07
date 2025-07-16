namespace Movie.Core.Entities
{
    public class FilmActor : EntityBase
    {
        public int FilmId { get; set; }
        public int ActorId { get; set; }
        public Film Film { get; set; } = null!;
        public Actor Actor { get; set; } = null!;
    }
}
