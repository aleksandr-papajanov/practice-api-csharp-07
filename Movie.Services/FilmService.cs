using Microsoft.EntityFrameworkCore;
using Movie.Contracts;
using Movie.Core.DTOs.Films;
using Movie.Core.DTOs.Mappers;
using Movie.Core.Entities;
using Movie.Core.Exceptions;
using Movie.Data.Infrastructure;

namespace Movie.Services
{
    public class FilmService : IFilmService
    {
        private readonly IRepository<Film> _movieRepository;
        private readonly IRepository<FilmDetails> _detailsRepository;

        public FilmService(
            IRepository<Film> movieRepository,
            IRepository<FilmDetails> detailsRepository)
        {
            _movieRepository = movieRepository;
            _detailsRepository = detailsRepository;
        }

        public async Task<IEnumerable<FilmDTO>> GetAllFilmsAsync(GetAllFilmsDTO request)
        {
            var query = _movieRepository.All
                .Include(e => e.FilmActors)
                    .ThenInclude(e => e.Actor)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Genre))
                query = query.Where(e => e.Genre == request.Genre);

            if (request.Year is not null)
                query = query.Where(e => e.Year == request.Year);

            if (!string.IsNullOrWhiteSpace(request.Actor))
                query = query
                    .Where(e => e.FilmActors.Any(e =>
                        e.Actor.Name.ToLower().Contains(request.Actor.ToLower()))); // StringComparison here is not available in EF Core LINQ

            query = query
                .Skip(request.Skip)
                .Take(request.Take);

            var movies = await query.ToListAsync();

            return movies.Select(e => e.ToDTO()).ToList();
        }

        public async Task<FilmDTO> GetFilmAsync(int id)
        {
            var movie = await _movieRepository.GetAsync(id)
                ?? throw new NotFoundAppException($"Movie with ID {id} not found.");

            return movie.ToDTO();
        }

        public async Task<FilmDetailsDTO> GetFilmDetailsAsync(int id)
        {
            var movie = await _movieRepository.All
                .Include(e => e.Details)
                .Include(e => e.FilmActors)
                    .ThenInclude(e => e.Actor)
                .Include(e => e.Reviews)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync()
                    ?? throw new NotFoundAppException($"Movie with ID {id} not found.");

            return movie.ToDetailsDTO();
        }

        public async Task<FilmDTO> CreateFilmAsync(CreateFilmDTO request)
        {
            var movie = request.ToEntity();
            var details = request.ToDetailsEntity();

            await EnsureMovieUniqAsync(movie.Title);
            await _movieRepository.AddAsync(movie);

            // Ensure that the details are linked to the movie and save
            details.FilmId = movie.Id;
            await _detailsRepository.AddAsync(details);

            return movie.ToDTO();
        }

        public async Task UpdateFilmAsync(int id, UpdateFilmDTO request)
        {
            var movie = await _movieRepository.All
                .Include(e => e.Details)
                .FirstOrDefaultAsync(e => e.Id == id)
                    ?? throw new NotFoundAppException($"Movie with ID {id} not found.");

            // Update movie properties
            if (request.Title is not null)
            {
                await EnsureMovieUniqAsync(request.Title);
                movie.Title = request.Title;
            }

            if (request.Genre is not null)
                movie.Genre = request.Genre;

            if (request.Year.HasValue)
                movie.Year = request.Year.Value;

            if (request.Duration.HasValue)
                movie.Duration = request.Duration.Value;

            await _movieRepository.UpdateAsync(movie);

            // Update movie details
            if (movie.Details is null)
            {
                // Create new details if they don't exist
                if (request.Synopsis is null || request.Language is null || request.Budget is null)
                {
                    throw new BadRequestAppException("All details must be provided when initializing movie details.");
                }

                var details = new FilmDetails
                {
                    FilmId = movie.Id,
                    Synopsis = request.Synopsis,
                    Language = request.Language,
                    Budget = request.Budget.Value
                };

                movie.Details = details;
                await _detailsRepository.AddAsync(details);
            }
            else
            {
                // Update existing details
                if (request.Synopsis is not null)
                    movie.Details.Synopsis = request.Synopsis;

                if (request.Language is not null)
                    movie.Details.Language = request.Language;

                if (request.Budget.HasValue)
                    movie.Details.Budget = request.Budget.Value;

                await _detailsRepository.UpdateAsync(movie.Details);
            }
        }

        public async Task DeleteFilmAsync(int id)
        {
            var movie = await _movieRepository.All
                .FirstOrDefaultAsync(e => e.Id == id)
                    ?? throw new NotFoundAppException($"Movie with ID {id} not found.");

            await _movieRepository.DeleteAsync(movie);
        }

        private async Task EnsureMovieUniqAsync(string title)
        {
            var exists = await _movieRepository.All.AnyAsync(e => e.Title == title);

            if (exists)
            {
                throw new BadRequestAppException($"Movie with title '{title}' already exists.");
            }
        }
    }
}