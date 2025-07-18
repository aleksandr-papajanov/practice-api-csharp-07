using Microsoft.EntityFrameworkCore;
using Movie.Contracts;
using Movie.Core.Abstractions;
using Movie.Core.DTOs.Reviews;
using Movie.Core.Exceptions;
using Movie.Services.Mappers;

namespace Movie.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _uow;


        public ReviewService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }


        public async Task<ReviewDTO> GetReviewAsync(int id)
        {
            var review = await _uow.ReviewRepository.GetOrThrowAsync(id);
            return review.ToDTO();
        }

        public async Task<ReviewDTO> CreateReviewAsync(CreateReviewDTO request)
        {
            var review = request.ToEntity();

            _uow.FilmRepository.EnsureExists(review.FilmId);
            _uow.ReviewRepository.EnsureIsNotContributed(review.FilmId, review.ReviewerName);

            _uow.ReviewRepository.Add(review);
            await _uow.CompleteAsync();

            return review.ToDTO();
        }

        public async Task UpdateReviewAsync(int id, UpdateReviewDTO request)
        {
            var review = await _uow.ReviewRepository.GetOrThrowAsync(id);

            if (request.ReviewerName is not null)
                review.ReviewerName = request.ReviewerName;

            if (request.Comment is not null)
                review.Comment = request.Comment;

            if (request.Rating.HasValue)
                review.Rating = request.Rating.Value;

            _uow.ReviewRepository.Update(review);
            await _uow.CompleteAsync();
        }

        public async Task DeleteReviewAsync(int id)
        {
            var review = await _uow.ReviewRepository.GetOrThrowAsync(id);
            _uow.ReviewRepository.Delete(review);
            await _uow.CompleteAsync();
        }
    }
}
