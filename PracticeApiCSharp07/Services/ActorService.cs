using Microsoft.EntityFrameworkCore;
using PracticeApiCSharp07.DTOs.Actors;
using PracticeApiCSharp07.DTOs.Mappers;
using PracticeApiCSharp07.Entities;
using PracticeApiCSharp07.Helpers;
using PracticeApiCSharp07.Infrastructure;

namespace PracticeApiCSharp07.Services
{
    internal class ActorService : IActorService
    {
        private readonly IRepository<Movie> _movieRepository;
        private readonly IRepository<Actor> _actorRepository;
        private readonly IRepository<MovieActor> _movieActorRepository;

        public ActorService(
            IRepository<Movie> movieRepository,
            IRepository<Actor> actorRepository,
            IRepository<MovieActor> movieActorRepository)
        {
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
            _movieActorRepository = movieActorRepository;
        }


        public async Task<IEnumerable<ActorDTO>> GetAllActorsAsync(GetAllActorsDTO request)
        {
            var query = _actorRepository.All
                .Include(e => e.MovieActors)
                    .ThenInclude(e => e.Movie)
                .AsQueryable();

            query = query
                .Skip(request.Skip)
                .Take(request.Take);

            var actors = await query.ToListAsync();

            return actors.Select(e => e.ToDTO()).ToList();
        }

        public async Task<ActorDTO> GetActorAsync(int id)
        {
            var actor = await _actorRepository.All
                .Include(e => e.MovieActors)
                    .ThenInclude(ma => ma.Movie)
                .FirstOrDefaultAsync(e => e.Id == id)
                    ?? throw new NotFoundAppException($"Actor with ID {id} not found.");

            return actor.ToDTO();
        }

        public async Task AssignActorToMovieAsync(int movieId, int actorId)
        {
            await EnsureMovieExistsAsync(movieId);
            await EnsureActorExistsAsync(actorId);

            var exists = await _movieActorRepository.All
                .AnyAsync(e => e.MovieId == movieId && e.ActorId == actorId);

            if (exists)
            {
                throw new BadRequestAppException($"Actor with ID {actorId} is already assigned to movie with ID {movieId}.");
            }

            var movieActor = new MovieActor
            {
                MovieId = movieId,
                ActorId = actorId
            };

            await _movieActorRepository.AddAsync(movieActor);
        }

        public async Task<ActorDTO> CreateActorAsync(CreateActorDTO request)
        {
            var actor = request.ToEntity();

            await EnsureActorUniqAsync(actor.Name);
            await _actorRepository.AddAsync(actor);

            return actor.ToDTO();
        }

        public async Task UpdateActorAsync(int id, UpdateActorDTO request)
        {
            var actor = await _actorRepository.GetAsync(id)
                ?? throw new NotFoundAppException($"Actor with ID {id} not found.");

            // Update actor properties
            if (request.Name is not null)
            {
                await EnsureActorUniqAsync(request.Name);
                actor.Name = request.Name;
            }

            if (request.BirthYear is not null)
                actor.BirthYear = (int)request.BirthYear;

            await _actorRepository.UpdateAsync(actor);
        }

        public async Task DeleteActorAsync(int id)
        {
            var actor = await _actorRepository.GetAsync(id)
                ?? throw new NotFoundAppException($"Actor with ID {id} not found.");

            await _actorRepository.DeleteAsync(actor);
        }

        private async Task EnsureMovieExistsAsync(int movieId)
        {
            var exists = await _movieRepository.All
                .AnyAsync(e => e.Id == movieId);

            if (!exists)
            {
                throw new NotFoundAppException($"Movie with ID {movieId} not found.");
            }
        }

        private async Task EnsureActorExistsAsync(int actorId)
        {
            var exists = await _actorRepository.All
                .AnyAsync(e => e.Id == actorId);

            if (!exists)
            {
                throw new NotFoundAppException($"Actor with ID {actorId} not found.");
            }
        }

        private async Task EnsureActorUniqAsync(string name)
        {
            var exists = await _actorRepository.All
                .AnyAsync(e => e.Name == name);

            if (exists)
            {
                throw new BadRequestAppException($"Actor with name '{name}' already exists.");
            }
        }
    }
}
