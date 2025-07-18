using Movie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Abstractions.Repositories
{
    public interface IFilmRepository : IRepository<Film>
    {
        void EnsureExists(int filmId);
        void EnsureUnique(string title);
    }
}
