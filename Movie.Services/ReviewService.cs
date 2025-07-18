using Microsoft.EntityFrameworkCore;
using Movie.Contracts;
using Movie.Core.Abstractions;
using Movie.Core.DTOs.Reviews;
using Movie.Services.Exceptions;
using Movie.Services.Mappers;

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
                 ?? throw new ReviewNotFoundAppException(id);

            return review.ToDTO();
        }

        public async Task<ReviewDTO> CreateReviewAsync(CreateReviewDTO request)
        {
            var review = request.ToEntity();

            await EnsureFilmExistsAsync(review.FilmId);
            await EnsureReviewUniqAsync(review.FilmId, review.ReviewerName);

            await _unitOfWork.ReviewRepository.AddAsync(review);

            return review.ToDTO();
        }

        public async Task UpdateReviewAsync(int id, UpdateReviewDTO request)
        {
            var review = await _unitOfWork.ReviewRepository.GetAsync(id)
                ?? throw new ReviewNotFoundAppException(id);

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
                ?? throw new ReviewNotFoundAppException(id);

            await _unitOfWork.ReviewRepository.DeleteAsync(review);
        }

        private async Task EnsureFilmExistsAsync(int filmId)
        {
            var exists = await _unitOfWork.FilmRepository.All
                .AnyAsync(e => e.Id == filmId);

            if (!exists)
            {
                throw new FilmNotFoundAppException(filmId);
            }
        }

        private async Task EnsureReviewUniqAsync(int filmId, string reviewer)
        {
            var exists = await _unitOfWork.ReviewRepository.All
                .AnyAsync(e => e.FilmId == filmId &&
                               e.ReviewerName == reviewer);

            if (!exists)
            {
                throw new ReviewerContributionConflictAppException(reviewer, filmId);
            }
        }
    }
}
