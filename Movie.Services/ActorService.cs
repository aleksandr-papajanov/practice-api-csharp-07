using Microsoft.EntityFrameworkCore;
using Movie.Contracts.Services;
using Movie.Core.Abstractions;
using Movie.Core.DTOs.Actors;
using Movie.Core.DTOs.Mappers;
using Movie.Core.Entities;
using Movie.Core.Exceptions;

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
                    .ThenInclude(ma => ma.Film)
                .FirstOrDefaultAsync(e => e.Id == id)
                    ?? throw new NotFoundAppException($"Actor with ID {id} not found.");

            return actor.ToDTO();
        }

        public async Task AssignActorToFilmAsync(int movieId, int actorId)
        {
            await EnsureMovieExistsAsync(movieId);
            await EnsureActorExistsAsync(actorId);

            var exists = await _unitOfWork.FilmActorRepository.All
                .AnyAsync(e => e.FilmId == movieId && e.ActorId == actorId);

            if (exists)
            {
                throw new BadRequestAppException($"Actor with ID {actorId} is already assigned to movie with ID {movieId}.");
            }

            var movieActor = new FilmActor
            {
                FilmId = movieId,
                ActorId = actorId
            };

            await _unitOfWork.FilmActorRepository.AddAsync(movieActor);
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
                ?? throw new NotFoundAppException($"Actor with ID {id} not found.");

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
                ?? throw new NotFoundAppException($"Actor with ID {id} not found.");

            await _unitOfWork.ActorRepository.DeleteAsync(actor);
        }

        private async Task EnsureMovieExistsAsync(int movieId)
        {
            var exists = await _unitOfWork.FilmRepository.All
                .AnyAsync(e => e.Id == movieId);

            if (!exists)
            {
                throw new NotFoundAppException($"Movie with ID {movieId} not found.");
            }
        }

        private async Task EnsureActorExistsAsync(int actorId)
        {
            var exists = await _unitOfWork.ActorRepository.All
                .AnyAsync(e => e.Id == actorId);

            if (!exists)
            {
                throw new NotFoundAppException($"Actor with ID {actorId} not found.");
            }
        }

        private async Task EnsureActorUniqAsync(string name)
        {
            var exists = await _unitOfWork.ActorRepository.All
                .AnyAsync(e => e.Name == name);

            if (exists)
            {
                throw new BadRequestAppException($"Actor with name '{name}' already exists.");
            }
        }
    }
}
