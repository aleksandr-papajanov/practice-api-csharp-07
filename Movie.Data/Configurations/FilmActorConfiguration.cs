using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Core.Entities;

namespace Movie.Data.Configurations
{
    public class FilmActorConfiguration : IEntityTypeConfiguration<FilmActor>
    {
        public void Configure(EntityTypeBuilder<FilmActor> e)
        {
            e.HasKey(op => new { op.FilmId, op.ActorId });
            e.ToTable("FilmActor");
        }
    }
}
