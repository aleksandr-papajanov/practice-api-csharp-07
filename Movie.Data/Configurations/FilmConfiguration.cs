using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Movie.Data.Configurations
{
    public class FilmConfiguration : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> e)
        {
            e.HasKey(e => e.Id);

            e.HasOne(e => e.Details)
             .WithOne(e => e.Film)
             .HasForeignKey<FilmDetails>(e => e.FilmId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(e => e.FilmGenre)
             .WithMany(e => e.Films)
             .HasForeignKey(e => e.FilmGenreId);

            e.HasMany(e => e.FilmActors)
             .WithOne(e => e.Film)
             .HasForeignKey(e => e.FilmId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(e => e.Reviews)
             .WithOne(e => e.Film)
             .HasForeignKey(e => e.FilmId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasIndex(e => e.Title)
             .IsUnique();

            e.ToTable("Film", t =>
            {
                t.HasCheckConstraint("CK_Film_Year", $"{nameof(Film.Year)} >= 1888");
                t.HasCheckConstraint("CK_Film_Duration", $"{nameof(Film.Duration)} > 0");
            });
        }
    }
}
