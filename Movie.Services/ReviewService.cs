using Microsoft.EntityFrameworkCore;
using Movie.Contracts.Services;
using Movie.Core.Abstractions;
using Movie.Core.DTOs.Mappers;
using Movie.Core.DTOs.Reviews;
using Movie.Core.Exceptions;

namespace Movie.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;


        public ReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ReviewDTO> GetReviewAsync(int id)
        {
            var review = await _unitOfWork.ReviewRepository.GetAsync(id)
                 ?? throw new NotFoundAppException($"Review with ID {id} not found.");

            return review.ToDTO();
        }

        public async Task<ReviewDTO> CreateReviewAsync(CreateReviewDTO request)
        {
            var review = request.ToEntity();

            await EnsureMovieExistsAsync(review.FilmId);
            await EnsureReviewUniqAsync(review.FilmId, review.ReviewerName);

            await _unitOfWork.ReviewRepository.AddAsync(review);

            return review.ToDTO();
        }

        public async Task UpdateReviewAsync(int id, UpdateReviewDTO request)
        {
            var review = await _unitOfWork.ReviewRepository.GetAsync(id)
                ?? throw new NotFoundAppException($"Review with ID {id} not found.");
            
            if (request.ReviewerName is not null)
                review.ReviewerName = request.ReviewerName;

            if (request.Comment is not null)
                review.Comment = request.Comment;

            if (request.Rating.HasValue)
                review.Rating = request.Rating.Value;

            await _unitOfWork.ReviewRepository.UpdateAsync(review);
        }

        public async Task DeleteReviewAsync(int id)
        {
            var review = await _unitOfWork.ReviewRepository.GetAsync(id)
                ?? throw new NotFoundAppException($"Review with ID {id} not found.");

            await _unitOfWork.ReviewRepository.DeleteAsync(review);
        }

        private async Task EnsureMovieExistsAsync(int movieId)
        {
            var exists = await _unitOfWork.FilmRepository.All
                .AnyAsync(e => e.Id == movieId);

            if (!exists)
            {
                throw new NotFoundAppException($"Movie with ID {movieId} not found.");
            }
        }

        private async Task EnsureReviewUniqAsync(int movieId, string reviewerName)
        {
            var exists = await _unitOfWork.ReviewRepository.All
                .AnyAsync(e => e.FilmId == movieId &&
                               e.ReviewerName == reviewerName);

            if (!exists)
            {
                throw new BadRequestAppException($"Review by {reviewerName} for movie ID {movieId} already exists.");
            }
        }
    }
}
