using PracticeApiCSharp07.DTOs.Movies;
using PracticeApiCSharp07.Entities;

namespace PracticeApiCSharp07.DTOs.Mappers
{
    internal static class MovieMapper
    {
        public static MovieDTO ToDTO(this Movie entity) => new MovieDTO
        {
            Id = entity.Id,
            Title = entity.Title,
            Genre = entity.Genre,
            Year = entity.Year,
            Duration = entity.Duration
        };

        public static MovieDetailsDTO ToDetailsDTO(this Movie entity) => new MovieDetailsDTO
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

        public static Movie ToEntity(this CreateMovieDTO dto) => new Movie
        {
            Title = dto.Title,
            Genre = dto.Genre,
            Year = dto.Year,
            Duration = dto.Duration
        };

        public static MovieDetails ToDetailsEntity(this CreateMovieDTO dto) => new MovieDetails
        {
            Language = dto.Language,
            Synopsis = dto.Synopsis,
            Budget = dto.Budget
        };
    }
}
