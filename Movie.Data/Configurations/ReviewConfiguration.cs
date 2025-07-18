using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Core.Entities;

namespace Movie.Data.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> e)
        {
            e.HasKey(e => e.Id);

            e.ToTable("Review", t =>
            {
                t.HasCheckConstraint("CK_Review_Rating", $"{nameof(Review.Rating)} BETWEEN 1 AND 5");
            });
        }
    }
}
