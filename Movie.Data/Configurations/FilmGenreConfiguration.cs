using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Core.Entities;
using Movie.Data.Migrations;
using System.Reflection.Emit;

namespace Movie.Data.Configurations
{
    public class FilmGenreConfiguration : IEntityTypeConfiguration<FilmGenre>
    {
        public void Configure(EntityTypeBuilder<FilmGenre> builder)
        {
            builder.HasKey(e => e.Id);
            builder
                .HasIndex(e => e.Name)
                .IsUnique();

            builder.HasData(Enum.GetValues<FilmGenres>()
                .Select(g => new FilmGenre
                {
                    Id = (int)g,
                    Name = g.ToString()
                }));

            builder.ToTable("FilmGenre");
        }
    }
}
