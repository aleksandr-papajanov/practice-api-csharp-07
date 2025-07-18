using Movie.Core.DTOs.Actors;
using Movie.Core.Entities;

namespace Movie.Services.Mappers
{
    public static class ActorMapper
    {
        public static ActorDTO ToDTO(this Actor entity) => new ActorDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            BirthYear = entity.BirthYear,
            Films = entity.Films.Select(e => e.Title).ToList()
        };

        public static Actor ToEntity(this CreateActorDTO dto) => new Actor
        {
            Name = dto.Name,
            BirthYear = dto.BirthYear
        };
    }
}
