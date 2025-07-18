using Movie.Core.Entities;

namespace Movie.Core.Abstractions.Repositories
{
    public interface IFilmGenreRepository : IRepository<FilmGenre>
    {
        Task<FilmGenre> GetOrThrowAsync(string name);
    }
}
