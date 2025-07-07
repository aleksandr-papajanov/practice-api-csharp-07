using Microsoft.EntityFrameworkCore;
using PracticeApiCSharp07.DTOs.Mappers;
using PracticeApiCSharp07.DTOs.Reviews;
using PracticeApiCSharp07.Entities;
using PracticeApiCSharp07.Helpers;
using PracticeApiCSharp07.Infrastructure;

namespace PracticeApiCSharp07.Services
{
    internal class ReviewService : IReviewService
    {
        private readonly IRepository<Movie> _movieRepository;
        private readonly IRepository<Review> _reviewRepository;

        public ReviewService(
            IRepository<Movie> movieRepository,
            IRepository<Review> reviewRepository)
        {
            _movieRepository = movieRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<ReviewDTO> GetReviewAsync(int id)
        {
            var review = await _reviewRepository.GetAsync(id)
                 ?? throw new NotFoundAppException($"Review with ID {id} not found.");

            return review.ToDTO();
        }

        public async Task<ReviewDTO> CreateReviewAsync(CreateReviewDTO request)
        {
            var review = request.ToEntity();

            await EnsureMovieExistsAsync(review.MovieId);
            await EnsureReviewUniqAsync(review.MovieId, review.ReviewerName);

            await _reviewRepository.AddAsync(review);

            return review.ToDTO();
        }

        public async Task UpdateReviewAsync(int id, UpdateReviewDTO request)
        {
            var review = await _reviewRepository.GetAsync(id)
                ?? throw new NotFoundAppException($"Review with ID {id} not found.");
            
            if (request.ReviewerName is not null)
                review.ReviewerName = request.ReviewerName;

            if (request.Comment is not null)
                review.Comment = request.Comment;

            if (request.Rating.HasValue)
                review.Rating = request.Rating.Value;

            await _reviewRepository.UpdateAsync(review);
        }

        public async Task DeleteReviewAsync(int id)
        {
            var review = await _reviewRepository.GetAsync(id)
                ?? throw new NotFoundAppException($"Review with ID {id} not found.");

            await _reviewRepository.DeleteAsync(review);
        }

        private async Task EnsureMovieExistsAsync(int movieId)
        {
            var exists = await _movieRepository.All
                .AnyAsync(e => e.Id == movieId);

            if (!exists)
            {
                throw new NotFoundAppException($"Movie with ID {movieId} not found.");
            }
        }

        private async Task EnsureReviewUniqAsync(int movieId, string reviewerName)
        {
            var exists = await _reviewRepository.All
                .AnyAsync(e => e.MovieId == movieId &&
                               e.ReviewerName == reviewerName);

            if (!exists)
            {
                throw new BadRequestAppException($"Review by {reviewerName} for movie ID {movieId} already exists.");
            }
        }
    }
}
