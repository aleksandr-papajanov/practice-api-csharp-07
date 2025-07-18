using Movie.Core.DTOs.Actors;
using Movie.Core.DTOs.Common;

namespace Movie.Contracts
{
    public interface IActorService
    {
        Task<PaginatedResult<ActorDTO>> GetAllActorsAsync(GetAllActorsDTO request);
        Task<ActorDTO> GetActorAsync(int id);
        Task<ActorDTO> CreateActorAsync(CreateActorDTO request);
        Task AssignActorToFilmAsync(int movieId, int actorId);
        Task UpdateActorAsync(int id, UpdateActorDTO request);
        Task DeleteActorAsync(int id);
    }
}
