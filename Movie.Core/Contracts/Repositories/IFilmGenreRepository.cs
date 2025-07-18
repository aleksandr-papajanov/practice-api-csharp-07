using Movie.Core.Entities;

namespace Movie.Core.Contracts.Repositories
{
    public interface IFilmGenreRepository : IRepository<FilmGenre>
    {
        Task<FilmGenre> GetOrThrowAsync(string name);
    }
}
