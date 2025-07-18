using Movie.Core.Contracts.Repositories;
using Movie.Core.Entities;
using Movie.Core.Exceptions;

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
                ?? throw new ActorNotFoundAppException(id);
        }

        public void EnsureUnique(string name, int? ignoreId = null)
        {
            if (_set.Any(e => e.Name == name && (ignoreId == null || e.Id != ignoreId)))
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
