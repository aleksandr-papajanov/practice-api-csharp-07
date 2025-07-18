using Movie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Contracts.Repositories
{
    public interface IFilmActorRepository : IRepository<FilmActor>
    {
        void EnsureUnique(int filmId, int actorId);
    }
}
