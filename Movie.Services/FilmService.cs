using Microsoft.EntityFrameworkCore;
using Movie.Contracts;
using Movie.Core.Abstractions;
using Movie.Core.DTOs.Common;
using Movie.Core.DTOs.Films;
using Movie.Core.Entities;
using Movie.Services.Exceptions;
using Movie.Services.Mappers;

namespace Movie.Services
{
    public class FilmService : IFilmService
    {
        private readonly IUnitOfWork _unitOfWork;


        public FilmService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<PaginatedResult<FilmDTO>> GetAllFilmsAsync(GetAllFilmsDTO request)
        {
            var query = _unitOfWork.FilmRepository.All
                .Include(e => e.FilmActors)
                    .ThenInclude(e => e.Actor)
                .Include(e => e.FilmGenre)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Genre))
                query = query.Where(e => e.FilmGenre.Name == request.Genre);

            if (request.Year is not null)
                query = query.Where(e => e.Year == request.Year);

            if (!string.IsNullOrWhiteSpace(request.Actor))
                query = query
                    .Where(e => e.FilmActors.Any(e =>
                        e.Actor.Name.ToLower().Contains(request.Actor.ToLower()))); // StringComparison here is not available in EF Core LINQ

            query = query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);

            var films = await query.ToListAsync();

            return new PaginatedResult<FilmDTO>(
                items: films.Select(e => e.ToDTO()).ToList(),
                totalCount: await _unitOfWork.ActorRepository.All.CountAsync(),
                currentPage: request.PageNumber,
                pageSize: request.PageSize);
        }

        public async Task<FilmDTO> GetFilmAsync(int id)
        {
            var film = await _unitOfWork.FilmRepository.GetAsync(id)
                ?? throw new FilmNotFoundAppException(id);

            return film.ToDTO();
        }

        public async Task<FilmDetailsDTO> GetFilmDetailsAsync(int id)
        {
            var film = await _unitOfWork.FilmRepository.All
                .Include(e => e.Details)
                .Include(e => e.FilmActors)
                    .ThenInclude(e => e.Actor)
                .Include(e => e.Reviews)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync()
                    ?? throw new FilmNotFoundAppException(id);

            return film.ToDetailsDTO();
        }

        public async Task<FilmDTO> CreateFilmAsync(CreateFilmDTO request)
        {
            var film = request.ToEntity();
            var details = request.ToDetailsEntity();

            // Ensure the genre exists
            var genre = await FindGenreAsync(request.Genre);
            film.FilmGenre = genre;

            await EnsureFilmUniqAsync(film.Title);
            await _unitOfWork.FilmRepository.AddAsync(film);

            // Ensure that the details are linked to the film and save
            details.FilmId = film.Id;
            await _unitOfWork.FilmDetailsRepository.AddAsync(details);

            return film.ToDTO();
        }

        public async Task UpdateFilmAsync(int id, UpdateFilmDTO request)
        {
            var film = await _unitOfWork.FilmRepository.All
                .Include(e => e.Details)
                .FirstOrDefaultAsync(e => e.Id == id)
                    ?? throw new FilmNotFoundAppException(id);

            // Update film properties
            if (request.Title is not null)
            {
                await EnsureFilmUniqAsync(request.Title);
                film.Title = request.Title;
            }

            if (request.Genre is not null)
            {
                var genre = await FindGenreAsync(request.Genre);
                film.FilmGenre = genre;
            }

            if (request.Year.HasValue)
                film.Year = request.Year.Value;

            if (request.Duration.HasValue)
                film.Duration = request.Duration.Value;

            await _unitOfWork.FilmRepository.UpdateAsync(film);

            // Update film details
            if (film.Details is null)
            {
                // Create new details if they don't exist
                if (request.Synopsis is null || request.Language is null || request.Budget is null)
                {
                    throw new FilmDetailsNotProvidedAppException();
                }

                var details = new FilmDetails
                {
                    FilmId = film.Id,
                    Synopsis = request.Synopsis,
                    Language = request.Language,
                    Budget = request.Budget.Value
                };

                film.Details = details;
                await _unitOfWork.FilmDetailsRepository.AddAsync(details);
            }
            else
            {
                // Update existing details
                if (request.Synopsis is not null)
                    film.Details.Synopsis = request.Synopsis;

                if (request.Language is not null)
                    film.Details.Language = request.Language;

                if (request.Budget.HasValue)
                    film.Details.Budget = request.Budget.Value;

                await _unitOfWork.FilmDetailsRepository.UpdateAsync(film.Details);
            }
        }

        public async Task DeleteFilmAsync(int id)
        {
            var film = await _unitOfWork.FilmRepository.All
                .FirstOrDefaultAsync(e => e.Id == id)
                    ?? throw new FilmNotFoundAppException(id);

            await _unitOfWork.FilmRepository.DeleteAsync(film);
        }

        private async Task EnsureFilmUniqAsync(string title)
        {
            var exists = await _unitOfWork.FilmRepository.All.AnyAsync(e => e.Title == title);

            if (exists)
            {
                throw new FilmTitleConflictAppException(title);
            }
        }
        
        private async Task<FilmGenre> FindGenreAsync(string genre)
        {
            var exists = await _unitOfWork.FilmGenreRepository.All.FirstOrDefaultAsync(e => e.Name == genre);

            if (exists == null)
            {
                throw new GenreNotExistsAppException(genre);
            }

            return exists;
        }
    }
}