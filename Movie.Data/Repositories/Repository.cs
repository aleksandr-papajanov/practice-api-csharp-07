using Microsoft.EntityFrameworkCore;
using Movie.Core.Abstractions.Repositories;
using Movie.Core.Entities;

namespace Movie.Data.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : EntityBase
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _set;

        public virtual IQueryable<T> All => _set.AsQueryable();

        public Repository(AppDbContext context)
        {
            _context = context;
            _set = _context.Set<T>();

        }

        public virtual async Task<T?> GetAsync(int id)
        {
            return await _set.FindAsync(id);
        }

        public virtual void Add(T item)
        {
            _set.AddAsync(item);
        }

        public virtual void Delete(T item)
        {
            _set.Remove(item);
        }


        public virtual void Update(T item)
        {
            _set.Update(item);
        }
    }
}
