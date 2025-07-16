using Movie.Core.DTOs.Reviews;
using Movie.Core.Entities;

namespace Movie.Core.DTOs.Mappers
{
    public static class ReviewMapper
    {
        public static ReviewDTO ToDTO(this Review entity) => new ReviewDTO
        {
            Id = entity.Id,
            ReviewerName = entity.ReviewerName,
            Comment = entity.Comment,
            Rating = entity.Rating
        };

        public static Review ToEntity(this CreateReviewDTO dto) => new Review
        {
            FilmId = dto.FilmId,
            ReviewerName = dto.ReviewerName,
            Comment = dto.Comment,
            Rating = dto.Rating
        };
    }
}
