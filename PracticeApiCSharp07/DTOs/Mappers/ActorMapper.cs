using PracticeApiCSharp07.DTOs.Actors;
using PracticeApiCSharp07.DTOs.Movies;
using PracticeApiCSharp07.Entities;

namespace PracticeApiCSharp07.DTOs.Mappers
{
    internal static class ActorMapper
    {
        public static ActorDTO ToDTO(this Actor entity) => new ActorDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            BirthYear = entity.BirthYear,
            Movies = entity.Movies.Select(e => e.Title).ToList()
        };

        public static Actor ToEntity(this CreateActorDTO dto) => new Actor
        {
            Name = dto.Name,
            BirthYear = dto.BirthYear
        };
    }
}
