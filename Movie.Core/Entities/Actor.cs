using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Core.Entities
{
    public class Actor : EntityBase
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int BirthYear { get; set; }

        // Navigation properties
        public ICollection<FilmActor> FilmActors { get; set; } = [];

        [NotMapped]
        public IEnumerable<Film> Films => FilmActors.Select(x => x.Film);
    }
}
