using Microsoft.EntityFrameworkCore;
using Movie.Core.Contracts.Repositories;
using Movie.Core.Entities;
using Movie.Core.Exceptions.NotFound;

namespace Movie.Data.Repositories
{
    public class FilmGenreRepository : Repository<FilmGenre>, IFilmGenreRepository
    {
        public FilmGenreRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<FilmGenre> GetOrThrowAsync(string name)
        {
            return await _set.FirstOrDefaultAsync(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                ?? throw new FilmGenreNotFoundAppException(name);
        }
    }
}
