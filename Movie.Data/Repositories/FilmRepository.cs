using Movie.Core.Abstractions;
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
    public class FilmRepository : Repository<Film>, IFilmRepository
    {
        public FilmRepository(AppDbContext context) : base(context)
        {
        }

        public void EnsureExists(int filmId)
        {
            if (!_set.Any(e => e.Id == filmId))
            {
                throw new FilmNotFoundAppException(filmId);
            }
        }

        public void EnsureUnique(string title)
        {
            if (_set.Any(e => e.Title == title))
            {
                throw new FilmTitleConflictAppException(title);
            }
        }
    }
}
