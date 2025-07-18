using Microsoft.EntityFrameworkCore;
using Movie.Contracts;
using Movie.Core.Abstractions;
using Movie.Core.DTOs.Actors;
using Movie.Core.Entities;
using Movie.Services.Exceptions;
using Movie.Services.Mappers;

namespace Movie.Services
{
    public class ActorService : IActorService
    {
        private readonly IUnitOfWork _unitOfWork;


        public ActorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<ActorDTO>> GetAllActorsAsync(GetAllActorsDTO request)
        {
            var query = _unitOfWork.ActorRepository.All
                .Include(e => e.FilmActors)
                    .ThenInclude(e => e.Film)
                .AsQueryable();

            query = query
                .Skip(request.Skip)
                .Take(request.Take);

            var actors = await query.ToListAsync();

            return actors.Select(e => e.ToDTO()).ToList();
        }

        public async Task<ActorDTO> GetActorAsync(int id)
        {
            var actor = await _unitOfWork.ActorRepository.All
                .Include(e => e.FilmActors)
                    .ThenInclude(fa => fa.Film)
                .FirstOrDefaultAsync(e => e.Id == id)
                    ?? throw new ActorNotFoundAppException(id);

            return actor.ToDTO();
        }

        public async Task AssignActorToFilmAsync(int filmId, int actorId)
        {
            var film = await _unitOfWork.FilmRepository.All
                .Include(e => e.FilmActors)
                .FirstOrDefaultAsync(e => e.Id == filmId);

            if (film is null)
            {
                throw new FilmNotFoundAppException(filmId);
            }

            if (film.FilmGenreId == (int)FilmGenres.Documentary && film.FilmActors.Count > 9)
            {
                throw new DocumentaryFilmMaxActorsExceededAppException(filmId, 10);
            }

            await EnsureActorExistsAsync(actorId);

            var exists = await _unitOfWork.FilmActorRepository.All
                .AnyAsync(e => e.FilmId == filmId && e.ActorId == actorId);

            if (exists)
            {
                throw new ActorFilmAssignmentConflictAppException(actorId, filmId);
            }

            var filmActor = new FilmActor
            {
                FilmId = filmId,
                ActorId = actorId
            };

            await _unitOfWork.FilmActorRepository.AddAsync(filmActor);
        }

        public async Task<ActorDTO> CreateActorAsync(CreateActorDTO request)
        {
            var actor = request.ToEntity();

            await EnsureActorUniqAsync(actor.Name);
            await _unitOfWork.ActorRepository.AddAsync(actor);

            return actor.ToDTO();
        }

        public async Task UpdateActorAsync(int id, UpdateActorDTO request)
        {
            var actor = await _unitOfWork.ActorRepository.GetAsync(id)
                ?? throw new ActorNotFoundAppException(id);

            // Update actor properties
            if (request.Name is not null)
            {
                await EnsureActorUniqAsync(request.Name);
                actor.Name = request.Name;
            }

            if (request.BirthYear is not null)
                actor.BirthYear = (int)request.BirthYear;

            await _unitOfWork.ActorRepository.UpdateAsync(actor);
        }

        public async Task DeleteActorAsync(int id)
        {
            var actor = await _unitOfWork.ActorRepository.GetAsync(id)
                ?? throw new ActorNotFoundAppException(id);

            await _unitOfWork.ActorRepository.DeleteAsync(actor);
        }

        private async Task EnsureFilmExistsAsync(int filmId)
        {
            var exists = await _unitOfWork.FilmRepository.All
                .AnyAsync(e => e.Id == filmId);

            if (!exists)
            {
                throw new FilmNotFoundAppException(filmId);
            }
        }

        private async Task EnsureActorExistsAsync(int actorId)
        {
            var exists = await _unitOfWork.ActorRepository.All
                .AnyAsync(e => e.Id == actorId);

            if (!exists)
            {
                throw new ActorNotFoundAppException(actorId);
            }
        }

        private async Task EnsureActorUniqAsync(string name)
        {
            var exists = await _unitOfWork.ActorRepository.All
                .AnyAsync(e => e.Name == name);

            if (exists)
            {
                throw new ActorNameConflictAppException(name);
            }
        }
    }
}
