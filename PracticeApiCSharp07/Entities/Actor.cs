using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeApiCSharp07.Entities
{
    internal class Actor : EntityBase
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int BirthYear { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; } = [];

        [NotMapped]
        public IEnumerable<Movie> Movies => MovieActors.Select(x => x.Movie);
    }
}
