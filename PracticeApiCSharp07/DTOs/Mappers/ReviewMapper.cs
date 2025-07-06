using PracticeApiCSharp07.DTOs.Actors;
using PracticeApiCSharp07.DTOs.Movies;
using PracticeApiCSharp07.DTOs.Reviews;
using PracticeApiCSharp07.Entities;

namespace PracticeApiCSharp07.DTOs.Mappers
{
    internal static class ReviewMapper
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
            MovieId = dto.MovieId,
            ReviewerName = dto.ReviewerName,
            Comment = dto.Comment,
            Rating = dto.Rating
        };
    }
}
