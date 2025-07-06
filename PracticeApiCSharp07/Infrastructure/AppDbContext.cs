using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using PracticeApiCSharp07.Entities;

namespace PracticeApiCSharp07.Infrastructure
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieDetails> MovieDetails { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Movie>(e =>
            {
                e.HasKey(e => e.Id);

                e.HasOne(e => e.Details)
                 .WithOne(e => e.Movie)
                 .HasForeignKey<MovieDetails>(e => e.MovieId)
                 .OnDelete(DeleteBehavior.Cascade);

                e.HasMany(e => e.MovieActors)
                 .WithOne(e => e.Movie)
                 .HasForeignKey(e => e.MovieId)
                 .OnDelete(DeleteBehavior.Cascade);

                e.HasMany(e => e.Reviews)
                 .WithOne(e => e.Movie)
                 .HasForeignKey(e => e.MovieId)
                 .OnDelete(DeleteBehavior.Cascade);

                e.HasIndex(e => e.Title)
                 .IsUnique();

                e.ToTable("Movie", t =>
                {
                    t.HasCheckConstraint("CK_Movie_Year", $"{nameof(Movie.Year)} >= 1888");
                    t.HasCheckConstraint("CK_Movie_Duration", $"{nameof(Movie.Duration)} > 0");
                });
            });

            builder.Entity<MovieDetails>(e =>
            {
                e.HasKey(e => e.Id);

                e.ToTable("MovieDetails", t =>
                {
                    t.HasCheckConstraint("CK_MovieDetails_Budget", $"{nameof(Entities.MovieDetails.Budget)} > 0");
                });
            });

            builder.Entity<Actor>(e =>
            {
                e.HasKey(e => e.Id);

                e.HasMany(e => e.MovieActors)
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
            
            builder.Entity<MovieActor>(e =>
            {
                e.HasKey(op => new { op.MovieId, op.ActorId });
                e.ToTable("MovieActor");
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
