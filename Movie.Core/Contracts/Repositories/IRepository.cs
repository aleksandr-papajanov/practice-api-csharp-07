using Movie.Core.Entities;

namespace Movie.Core.Contracts.Repositories
{
    public interface IRepository<T>
        where T : EntityBase
    {
        IQueryable<T> All { get; }
        Task<T?> GetAsync(int id);
        void Add(T item);
        void Update(T item);
        void Delete(T item);
    }
}
