using Movie.Core.DTOs.Reviews;

namespace Movie.Contracts.Services
{
    public interface IReviewService
    {
        Task<ReviewDTO> GetReviewAsync(int id);
        Task<ReviewDTO> CreateReviewAsync(CreateReviewDTO request);
        Task UpdateReviewAsync(int id, UpdateReviewDTO request);
        Task DeleteReviewAsync(int id);
    }
}
