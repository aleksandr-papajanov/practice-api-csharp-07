using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeApiCSharp07.Entities
{
    internal class Movie : EntityBase
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Genre { get; set; }
        public int Year { get; set; }
        public int Duration { get; set; }
        public MovieDetails Details { get; set; } = null!;
        public ICollection<Review> Reviews { get; set; } = [];
        public ICollection<MovieActor> MovieActors { get; set; } = [];

        [NotMapped]
        public IEnumerable<Actor> Actors => MovieActors.Select(x => x.Actor);
    }
}
