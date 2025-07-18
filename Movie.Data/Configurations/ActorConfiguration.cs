using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Core.Entities;

namespace Movie.Data.Configurations
{
    public class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> e)
        {
            e.HasKey(e => e.Id);

            e.HasIndex(e => e.Name)
             .IsUnique();

            e.HasMany(e => e.FilmActors)
             .WithOne(e => e.Actor)
             .HasForeignKey(e => e.ActorId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasIndex(e => e.Name)
             .IsUnique();

            e.ToTable("Actor", t =>
            {
                t.HasCheckConstraint("CK_Actor_BirthYear", $"{nameof(Actor.BirthYear)} >= 1835");
            });
        }
    }
}
