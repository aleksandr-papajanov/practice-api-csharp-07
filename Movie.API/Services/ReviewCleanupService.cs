using Microsoft.EntityFrameworkCore;
using Movie.Core.Contracts;

namespace Movie.API.Services
{
    public class ReviewCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(10);

        public ReviewCleanupService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                await CleanupReviewsAsync(unitOfWork);

                await Task.Delay(_interval, stoppingToken);
            }
        }

        private async Task CleanupReviewsAsync(IUnitOfWork unitOfWork)
        {
            var cutoffYear = DateTime.Now.Year - 20;

            var reviewsToDelete = unitOfWork.FilmRepository.All
                .Include(f => f.Reviews)
                .Where(f => f.Year < cutoffYear && f.Reviews.Count > 5)
                .SelectMany(f => f.Reviews
                    .OrderByDescending(film => film.Id) // Newest first
                    .Skip(5) // Keep latest 5 reviews
                );

            foreach (var review in reviewsToDelete)
            {
                unitOfWork.ReviewRepository.Delete(review);
            }

            await unitOfWork.CompleteAsync();
        }
    }
}
