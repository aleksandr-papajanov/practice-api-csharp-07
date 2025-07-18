#pragma warning disable CS1591

using Movie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Contracts.Repositories
{
    public interface IReviewRepository : IRepository<Review>
    {
        void EnsureExists(int filmId);
        void EnsureIsNotContributed(int filmId, string reviewer, int? ignoreId = null);
        Task<Review> GetOrThrowAsync(int id);
    }
}
