using Microsoft.EntityFrameworkCore;
using PracticeApiCSharp07.Entities;
using PracticeApiCSharp07.Infrastructure;

namespace PracticeApiCSharp07.Helpers
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

            if (_context.Movies.Any())
                return;

            for (int i = 0; i < DefaultMovieCount; i++)
            {
                if (!await AddMovie())
                    break;
            }
        }

        private async Task<bool> AddMovie()
        {
            var title = string.Empty;
            var retries = 0;

            // Make sure is unique
            do
            {
                title = RandomMovieDataGenerator.MovieTitle;

                if (retries++ >= MaxRetries)
                {
                    return false; // Avoid infinite loop
                }
            }
            while (await _context.Movies.AnyAsync(x => x.Title == title));

            var movie = new Movie
            {
                Title = title,
                Genre = RandomMovieDataGenerator.Genre,
                Year = RandomMovieDataGenerator.Year,
                Duration = RandomMovieDataGenerator.Duration,
            };

            _context.Movies.Add(movie);
            _context.SaveChanges();

            var details = new MovieDetails
            {
                MovieId = movie.Id,
                Synopsis = RandomMovieDataGenerator.Synopsis,
                Language = RandomMovieDataGenerator.Language,
                Budget = RandomMovieDataGenerator.Budget
            };

            _context.MovieDetails.Add(details);
            _context.SaveChanges();

            for (int j = 0; j < RandomMovieDataGenerator.ActorCount; j++)
            {
                if (!await AddActor(movie))
                    break;
            }

            for (int j = 0; j < RandomMovieDataGenerator.ReviewCount; j++)
            {
                if (!await AddReview(movie))
                    break;
            }

            return true;
        }

        private async Task<bool> AddActor(Movie movie)
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

            var movieActor = new MovieActor
            {
                MovieId = movie.Id,
                ActorId = actor.Id
            };

            _context.MovieActors.Add(movieActor);
            _context.SaveChanges();

            return true;
        }

        private async Task<bool> AddReview(Movie movie)
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
            while (retries++ < MaxRetries && await _context.Reviews.AnyAsync(x => x.MovieId == movie.Id && x.ReviewerName == name));

            var review = new Review
            {
                MovieId = movie.Id,
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
