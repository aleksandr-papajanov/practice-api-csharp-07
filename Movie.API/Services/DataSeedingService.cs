using Bogus;
using Bogus.DataSets;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Movie.API.Helpers;
using Movie.Contracts;
using Movie.Core.DTOs.Films;
using Movie.Core.Entities;
using Movie.Data;
using Movie.Data.Migrations;

namespace Movie.API.Services
{
    public class DataSeedingService : IHostedService
    {
        private const int DefaultMovieCount = 100;
        private const int DefaultActorCount = 50;
        private const int DefaultReviewCount = 1000;
        private const int DefaultGenreCount = 10;
        private const int MaxRetries = 20;

        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DataSeedingService> _logger;
        private AppDbContext _context;

        public DataSeedingService(IServiceProvider serviceProvider, ILogger<DataSeedingService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            if (!env.IsDevelopment()) return;

            _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            _context.Database.EnsureCreated();
            if (_context.Films.Any())
                return;

            for (int i = 0; i < DefaultMovieCount; i++)
            {
                await AddFilm();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }


        private async Task AddFilm()
        {
            var genre = await GetRandomGenre();

            Faker<Film> faker = new Faker<Film>("en").Rules((f, e) =>
            {
                e.Title = RandomMovieDataGenerator.FilmTitle;
                e.FilmGenre = genre;
                e.Year = RandomMovieDataGenerator.Year;
                e.Duration = f.Random.Int(40, 241);
                e.Details = new FilmDetails
                {
                    FilmId = e.Id, // This will be set after saving the film
                    Synopsis = RandomMovieDataGenerator.Synopsis,
                    Language = RandomMovieDataGenerator.Language,
                    Budget = genre.Name == "Documentary"
                        ? f.Random.Decimal(955, 1_000_000_000_000)
                        : f.Random.Decimal(955, 1_000_000_000)
                };
            });

            var film = faker.Generate();

            _context.Films.Add(film);
            _context.SaveChanges();

            Random rnd = new Random();
            var actorCount = genre.Name == "Documentary"
                ? rnd.Next(0, 10 + 1)
                : rnd.Next(0, 15 + 1);

            for (int j = 0; j < actorCount; j++)
            {
                await AddActor(film);
            }

            // Films older than 20 years will have max 5 reviews
            int reviewCount = film.Year < DateTime.UtcNow.Year - 20
                ? rnd.Next(0, 10 + 1)
                : rnd.Next(0, 5 + 1);

            for (int j = 0; j < reviewCount; j++)
            {
                await AddReview(film);
            }
        }

        private async Task AddActor(Film film)
        {
            Actor actor = null!;
            do
            {
                actor = await GetRandomActor();
            }
            while (await _context.FilmActors.AnyAsync(x => x.Film == film && x.Actor == actor));

            var role = new FilmActor
            {
                Film = film,
                Actor = actor
            };

            _context.FilmActors.Add(role);
            _context.SaveChanges();
        }

        private async Task<bool> AddReview(Film film)
        {
            Faker<Review> faker = new Faker<Review>("en").Rules((f, e) =>
            {
                e.Film = film;
                e.ReviewerName = f.Person.FullName;
                e.Comment = RandomMovieDataGenerator.Review;
                e.Rating = f.Random.Int(1, 5);
            });

            Review review = null!;

            // Make sure is unique
            do
            {
                review = faker.Generate();
            }
            while (await _context.Reviews.AnyAsync(x => x.Film == film && x.ReviewerName == review.ReviewerName));

            _context.Reviews.Add(review);
            _context.SaveChanges();

            return true;
        }


        private async Task<FilmGenre> GetRandomGenre()
        {
            // To avoid too many options we will limit the number of genres
            if (_context.FilmGenres.Count() > DefaultGenreCount)
            {
                var randomGenre = await _context.FilmGenres
                    .OrderBy(x => Guid.NewGuid())
                    .FirstAsync();

                return randomGenre;
            }

            var genreName = RandomMovieDataGenerator.Genre;

            var existing = await _context.FilmGenres.FirstOrDefaultAsync(x => x.Name == genreName);
            if (existing != null)
            {
                return existing;
            }

            var genreEntity = new FilmGenre
            {
                Name = genreName
            };

            _context.FilmGenres.Add(genreEntity);
            _context.SaveChanges();

            return genreEntity;
        }

        private async Task<Actor> GetRandomActor()
        {
            // To avoid too many options we will limit the number of actors
            if (_context.Actors.Count() > DefaultActorCount)
            {
                var randomActor = await _context.Actors
                    .OrderBy(x => Guid.NewGuid())
                    .FirstAsync();

                return randomActor;
            }

            Faker<Actor> faker = new Faker<Actor>("en").Rules((f, e) =>
            {
                e.Name = f.Person.FullName;
                e.BirthYear = RandomMovieDataGenerator.Year - f.Random.Int(0, 20); // Actors are usually a little older than the film
            });

            var actor = faker.Generate();

            _context.Actors.Add(actor);
            _context.SaveChanges();

            return actor;
        }
    }
}
