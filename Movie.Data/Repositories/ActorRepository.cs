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
    public class ActorRepository : Repository<Actor>, IActorRepository
    {
        public ActorRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Actor> GetOrThrowAsync(int id)
        {
            return await base.GetAsync(id)
                ?? throw new ReviewNotFoundAppException(id);
        }

        public void EnsureUnique(string name)
        {
            if (_set.Any(e => e.Name == name))
            {
                throw new ActorNameConflictAppException(name);
            }
        }

        public void EnsureExists(int actorId)
        {
            if (!_set.Any(e => e.Id == actorId))
            {
                throw new ActorNotFoundAppException(actorId);
            }
        }
    }
}
