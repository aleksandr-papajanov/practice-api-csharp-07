using Movie.Core.Entities;
using Movie.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Contracts.Repositories
{
    public interface IActorRepository : IRepository<Actor>
    {
        public void EnsureUnique(string name, int? ignoreId = null);
        public void EnsureExists(int actorId);
        Task<Actor> GetOrThrowAsync(int id);
    }
}
