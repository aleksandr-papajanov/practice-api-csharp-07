using Movie.Core.Contracts.Repositories;
using Movie.Core.Entities;
using Movie.Core.Exceptions;
using Movie.Core.Exceptions.Conflict;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Data.Repositories
{
    public class FilmActorRepository : Repository<FilmActor>, IFilmActorRepository
    {
        public FilmActorRepository(AppDbContext context) : base(context)
        {
        }

        public void EnsureUnique(int filmId, int actorId)
        {
            if (_set.Any(e => e.FilmId == filmId && e.ActorId == actorId))
            {
                throw new ActorFilmAssignmentConflictAppException(actorId, filmId);
            }
        }
    }
}
