using Microsoft.EntityFrameworkCore;
using Movie.Contracts;
using Movie.Core.Contracts;
using Movie.Core.DTOs.Actors;
using Movie.Core.DTOs.Common;
using Movie.Core.Entities;
using Movie.Core.Exceptions.Conflict;
using Movie.Core.Exceptions.NotFound;
using Movie.Services.Mappers;

namespace Movie.Services
{
    public class ActorService : IActorService
    {
        private readonly IUnitOfWork _uow;


        public ActorService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }


        public async Task<PaginatedResult<ActorDTO>> GetAllActorsAsync(GetAllActorsDTO request)
        {
            var query = _uow.ActorRepository.All
                .Include(e => e.FilmActors)
                    .ThenInclude(e => e.Film)
                .AsQueryable();

            var totalCount = await query.CountAsync();

            query = query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);

            var actors = await query.ToListAsync();

            return new PaginatedResult<ActorDTO>(
                items: actors.Select(e => e.ToDTO()).ToList(),
                totalCount: totalCount,
                currentPage: request.PageNumber,
                pageSize: request.PageSize);
        }

        public async Task<ActorDTO> GetActorAsync(int id)
        {
            var actor = await _uow.ActorRepository.All
                .Include(e => e.FilmActors)
                    .ThenInclude(fa => fa.Film)
                .FirstOrDefaultAsync(e => e.Id == id)
                    ?? throw new ActorNotFoundAppException(id);

            return actor.ToDTO();
        }

        public async Task AssignActorToFilmAsync(int filmId, int actorId)
        {
            var film = await _uow.FilmRepository.All
                .Include(e => e.FilmActors)
                .FirstOrDefaultAsync(e => e.Id == filmId)
                    ?? throw new FilmNotFoundAppException(filmId);

            if (film.FilmGenreId == (int)FilmGenres.Documentary && film.FilmActors.Count > 9)
            {
                throw new DocumentaryFilmMaxActorsExceededAppException(filmId, 10);
            }

            _uow.ActorRepository.EnsureExists(actorId);
            _uow.FilmActorRepository.EnsureUnique(filmId, actorId);

            var filmActor = new FilmActor
            {
                FilmId = filmId,
                ActorId = actorId
            };

            _uow.FilmActorRepository.Add(filmActor);
            await _uow.CompleteAsync();
        }

        public async Task<ActorDTO> CreateActorAsync(CreateActorDTO request)
        {
            var actor = request.ToEntity();

            _uow.ActorRepository.EnsureUnique(actor.Name);
            _uow.ActorRepository.Add(actor);
            await _uow.CompleteAsync();

            return actor.ToDTO();
        }

        public async Task UpdateActorAsync(int id, UpdateActorDTO request)
        {
            var actor = await _uow.ActorRepository.GetOrThrowAsync(id);

            // Update actor properties
            if (request.Name is not null)
            {
                _uow.ActorRepository.EnsureUnique(actor.Name, id);
                actor.Name = request.Name;
            }

            if (request.BirthYear is not null)
                actor.BirthYear = (int)request.BirthYear;

            _uow.ActorRepository.Update(actor);
            await _uow.CompleteAsync();
        }

        public async Task DeleteActorAsync(int id)
        {
            var actor = await _uow.ActorRepository.GetOrThrowAsync(id);
            _uow.ActorRepository.Delete(actor);
            await _uow.CompleteAsync();
        }
    }
}
