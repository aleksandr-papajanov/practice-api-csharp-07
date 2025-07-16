using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Core.Entities
{
    public class Film : EntityBase
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Genre { get; set; }
        public int Year { get; set; }
        public int Duration { get; set; }
        public FilmDetails Details { get; set; } = null!;
        public ICollection<Review> Reviews { get; set; } = [];
        public ICollection<FilmActor> FilmActors { get; set; } = [];

        [NotMapped]
        public IEnumerable<Actor> Actors => FilmActors.Select(x => x.Actor);
    }
}
