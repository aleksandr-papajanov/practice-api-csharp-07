using Movie.Core.DTOs.Films;
using Movie.Core.Entities;

namespace Movie.Core.DTOs.Mappers
{
    public static class FilmMapper
    {
        public static FilmDTO ToDTO(this Film entity) => new FilmDTO
        {
            Id = entity.Id,
            Title = entity.Title,
            Genre = entity.Genre,
            Year = entity.Year,
            Duration = entity.Duration
        };

        public static FilmDetailsDTO ToDetailsDTO(this Film entity) => new FilmDetailsDTO
        {
            Id = entity.Id,
            Title = entity.Title,
            Genre = entity.Genre,
            Year = entity.Year,
            Duration = entity.Duration,
            Synopsis = entity.Details.Synopsis,
            Language = entity.Details.Language,
            Budget = entity.Details.Budget,
            Actors = entity.Actors.Select(e => e.Name).ToList(),
            Reviews = entity.Reviews.Select(e => e.ToDTO())
        };

        public static Film ToEntity(this CreateFilmDTO dto) => new Film
        {
            Title = dto.Title,
            Genre = dto.Genre,
            Year = dto.Year,
            Duration = dto.Duration
        };

        public static FilmDetails ToDetailsEntity(this CreateFilmDTO dto) => new FilmDetails
        {
            Language = dto.Language,
            Synopsis = dto.Synopsis,
            Budget = dto.Budget
        };
    }
}
