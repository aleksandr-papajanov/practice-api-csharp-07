using Microsoft.EntityFrameworkCore;
using Movie.Core.Entities;
using Movie.Data.Infrastructure;

namespace Movie.API.Helpers
{
    internal class DataSeeder
    {
        private const int DefaultMovieCount = 100;
        private const int MaxRetries = 20;
        private readonly AppDbContext _context;

        public DataSeeder(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            _context.Database.EnsureCreated();

            if (_context.Films.Any())
                return;

            for (int i = 0; i < DefaultMovieCount; i++)
            {
                if (!await AddFilm())
                    break;
            }
        }

        private async Task<bool> AddFilm()
        {
            var title = string.Empty;
            var retries = 0;

            // Make sure is unique
            do
            {
                title = RandomMovieDataGenerator.FilmTitle;

                if (retries++ >= MaxRetries)
                {
                    return false; // Avoid infinite loop
                }
            }
            while (await _context.Films.AnyAsync(x => x.Title == title));

            var film = new Film
            {
                Title = title,
                Genre = RandomMovieDataGenerator.Genre,
                Year = RandomMovieDataGenerator.Year,
                Duration = RandomMovieDataGenerator.Duration,
            };

            _context.Films.Add(film);
            _context.SaveChanges();

            var details = new FilmDetails
            {
                FilmId = film.Id,
                Synopsis = RandomMovieDataGenerator.Synopsis,
                Language = RandomMovieDataGenerator.Language,
                Budget = RandomMovieDataGenerator.Budget
            };

            _context.FilmDetails.Add(details);
            _context.SaveChanges();

            for (int j = 0; j < RandomMovieDataGenerator.ActorCount; j++)
            {
                if (!await AddActor(film))
                    break;
            }

            for (int j = 0; j < RandomMovieDataGenerator.ReviewCount; j++)
            {
                if (!await AddReview(film))
                    break;
            }

            return true;
        }

        private async Task<bool> AddActor(Film film)
        {
            var name = string.Empty;
            var retries = 0;

            // Make sure is unique
            do
            {
                name = RandomMovieDataGenerator.FullName;

                if (retries++ >= MaxRetries)
                {
                    return false; // Avoid infinite loop
                }
            }
            while (retries++ < MaxRetries && await _context.Actors.AnyAsync(x => x.Name == name));

            var actor = new Actor
            {
                Name = name,
                BirthYear = RandomMovieDataGenerator.Year
            };

            _context.Actors.Add(actor);
            _context.SaveChanges();

            var movieActor = new FilmActor
            {
                FilmId = film.Id,
                ActorId = actor.Id
            };

            _context.FilmActors.Add(movieActor);
            _context.SaveChanges();

            return true;
        }

        private async Task<bool> AddReview(Film film)
        {
            var name = string.Empty;
            var retries = 0;

            // Make sure is unique
            do
            {
                name = RandomMovieDataGenerator.FullName;

                if (retries++ >= MaxRetries)
                {
                    return false; // Avoid infinite loop
                }
            }
            while (retries++ < MaxRetries && await _context.Reviews.AnyAsync(x => x.FilmId == film.Id && x.ReviewerName == name));

            var review = new Review
            {
                FilmId = film.Id,
                ReviewerName = name,
                Comment = RandomMovieDataGenerator.Review,
                Rating = RandomMovieDataGenerator.Rating
            };

            _context.Reviews.Add(review);
            _context.SaveChanges();

            return true;
        }
    }
}
