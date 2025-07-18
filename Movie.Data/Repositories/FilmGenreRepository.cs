using Microsoft.EntityFrameworkCore;
using Movie.Core.Abstractions.Repositories;
using Movie.Core.Entities;
using Movie.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                ?? throw new FilmGenreNotExistsAppException(name);
        }
    }
}
