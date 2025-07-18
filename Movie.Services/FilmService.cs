using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Movie.API.Helpers;
using Movie.Contracts;
using Movie.Core.Abstractions;
using Movie.Core.DTOs.Common;
using Movie.Core.DTOs.Films;
using Movie.Core.Entities;
using Movie.Core.Exceptions;
using Movie.Services.Mappers;

namespace Movie.Services
{
    public class FilmService : IFilmService
    {
        private readonly IUnitOfWork _uow;


        public FilmService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }


        public async Task<PaginatedResult<FilmDTO>> GetAllFilmsAsync(GetAllFilmsDTO request)
        {
            var query = _uow.FilmRepository.All
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
                totalCount: await _uow.ActorRepository.All.CountAsync(),
                currentPage: request.PageNumber,
                pageSize: request.PageSize);
        }

        public async Task<FilmDTO> GetFilmAsync(int id)
        {
            var film = await _uow.FilmRepository.All
                .Include(e => e.FilmGenre)
                .FirstOrDefaultAsync(e => e.Id == id)
                    ?? throw new FilmNotFoundAppException(id);

            return film.ToDTO();
        }

        public async Task<FilmDetailsDTO> GetFilmDetailsAsync(int id)
        {
            var film = await _uow.FilmRepository.All
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
            _uow.FilmRepository.EnsureUnique(request.Title);
            var genre = await _uow.FilmGenreRepository.GetOrThrowAsync(request.Genre);

            var film = request.ToEntity();
            film.FilmGenre = genre;
            film.Details = request.ToDetailsEntity();

            _uow.FilmRepository.Add(film);
            await _uow.CompleteAsync();

            return film.ToDTO();
        }

        public async Task UpdateFilmAsync(int id, UpdateFilmDTO request)
        {
            var film = await _uow.FilmRepository.All
                .Include(e => e.Details)
                .FirstOrDefaultAsync(e => e.Id == id)
                    ?? throw new FilmNotFoundAppException(id);

            // Update film properties
            if (request.Title is not null)
            {
                _uow.FilmRepository.EnsureUnique(request.Title);
                film.Title = request.Title;
            }

            if (request.Genre is not null)
            {
                var genre = await _uow.FilmGenreRepository.GetOrThrowAsync(request.Genre);
                film.FilmGenre = genre;
            }

            if (request.Year.HasValue)
                film.Year = request.Year.Value;

            if (request.Duration.HasValue)
                film.Duration = request.Duration.Value;

            _uow.FilmRepository.Update(film);

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
                _uow.FilmDetailsRepository.Add(details);
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

                _uow.FilmDetailsRepository.Update(film.Details);
            }

            await _uow.CompleteAsync();
        }

        public async Task UpdateFilmWithPatchDocumentAsync(int id, JsonPatchDocument<UpdateFilmDTO> patchDocument)
        {
            if (patchDocument is null)
                throw new PatchDocumentNullAppException();

            var dto = new UpdateFilmDTO();
            patchDocument.ApplyTo(dto);

            ValidationHelper.ValidateDto(dto);

            await UpdateFilmAsync(id, dto);
        }

        public async Task DeleteFilmAsync(int id)
        {
            var film = await _uow.FilmRepository.GetAsync(id)
                ?? throw new FilmNotFoundAppException(id);

            _uow.FilmRepository.Delete(film);
            await _uow.CompleteAsync();
        }
    }
}