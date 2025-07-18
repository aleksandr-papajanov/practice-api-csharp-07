#pragma warning disable CS1591

using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Core.Entities
{
    public class Film : EntityBase
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int Year { get; set; }
        public int Duration { get; set; }
        public FilmDetails Details { get; set; } = null!;

        // Foreign Keys
        public int FilmGenreId { get; set; }

        // Navigation Properties
        public FilmGenre FilmGenre { get; set; } = null!;
        public ICollection<Review> Reviews { get; set; } = [];
        public ICollection<FilmActor> FilmActors { get; set; } = [];

        [NotMapped]
        public IEnumerable<Actor> Actors => FilmActors.Select(x => x.Actor);
    }
}
