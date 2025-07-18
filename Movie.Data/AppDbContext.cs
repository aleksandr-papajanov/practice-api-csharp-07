using Microsoft.EntityFrameworkCore;
using Movie.Core.Entities;
using Movie.Data.Configurations;

namespace Movie.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmGenre> FilmGenres { get; set; }
        public DbSet<FilmDetails> FilmDetails { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<FilmActor> FilmActors { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new FilmConfiguration());
            builder.ApplyConfiguration(new FilmGenreConfiguration());
            builder.ApplyConfiguration(new FilmDetailsConfiguration());
            builder.ApplyConfiguration(new ActorConfiguration());
            builder.ApplyConfiguration(new FilmActorConfiguration());
            builder.ApplyConfiguration(new ReviewConfiguration());
        }
    }
}
