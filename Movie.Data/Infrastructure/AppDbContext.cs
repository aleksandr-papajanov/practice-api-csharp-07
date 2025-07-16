using Microsoft.EntityFrameworkCore;
using Movie.Core.Entities;

namespace Movie.Data.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmDetails> FilmDetails { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<FilmActor> FilmActors { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Film>(e =>
            {
                e.HasKey(e => e.Id);

                e.HasOne(e => e.Details)
                 .WithOne(e => e.Film)
                 .HasForeignKey<FilmDetails>(e => e.FilmId)
                 .OnDelete(DeleteBehavior.Cascade);

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
            });

            builder.Entity<FilmDetails>(e =>
            {
                e.HasKey(e => e.Id);

                e.ToTable("FilmDetails", t =>
                {
                    t.HasCheckConstraint("CK_FilmDetails_Budget", $"{nameof(Core.Entities.FilmDetails.Budget)} > 0");
                });
            });

            builder.Entity<Actor>(e =>
            {
                e.HasKey(e => e.Id);

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
            });
            
            builder.Entity<FilmActor>(e =>
            {
                e.HasKey(op => new { op.FilmId, op.ActorId });
                e.ToTable("FilmActor");
            });

            builder.Entity<Review>(e =>
            {
                e.HasKey(e => e.Id);

                e.ToTable("Review", t =>
                {
                    t.HasCheckConstraint("CK_Review_Rating", $"{nameof(Review.Rating)} BETWEEN 1 AND 5");
                });
            });
        }
    }
}
