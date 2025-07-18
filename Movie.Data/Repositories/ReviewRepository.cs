using Movie.Core.Contracts.Repositories;
using Movie.Core.Entities;
using Movie.Core.Exceptions;

namespace Movie.Data.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Review> GetOrThrowAsync(int id)
        {
            return await base.GetAsync(id)
                ?? throw new ReviewNotFoundAppException(id);
        }

        public void EnsureExists(int filmId)
        {
            if (!_set.Any(e => e.FilmId == filmId))
            {
                throw new ReviewNotFoundAppException(filmId);
            }
        }

        public void EnsureIsNotContributed(int filmId, string reviewer, int? ignoreId = null)
        {
            if (_set.Any(e => e.FilmId == filmId &&
                               e.ReviewerName == reviewer &&
                               (ignoreId == null || e.Id != ignoreId)))
            {
                throw new ReviewerContributionConflictAppException(reviewer, filmId);
            }
        }
    }
}
