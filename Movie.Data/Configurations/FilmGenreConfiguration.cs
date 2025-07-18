using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Core.Entities;

namespace Movie.Data.Configurations
{
    public class FilmGenreConfiguration : IEntityTypeConfiguration<FilmGenre>
    {
        public void Configure(EntityTypeBuilder<FilmGenre> e)
        {
            e.HasKey(e => e.Id);
            e.HasIndex(e => e.Name)
             .IsUnique();

            e.ToTable("FilmGenre");
        }
    }
}
