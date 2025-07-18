using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Core.Entities;

namespace Movie.Data.Configurations
{
    public class FilmDetailsConfiguration : IEntityTypeConfiguration<FilmDetails>
    {
        public void Configure(EntityTypeBuilder<FilmDetails> e)
        {
            e.HasKey(e => e.Id);

            e.ToTable("FilmDetails", t =>
            {
                t.HasCheckConstraint("CK_FilmDetails_Budget", $"{nameof(Core.Entities.FilmDetails.Budget)} > 0");
            });
        }
    }
}
