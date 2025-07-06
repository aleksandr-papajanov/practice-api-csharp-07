using PracticeApiCSharp07.DTOs.Actors;

namespace PracticeApiCSharp07.Services
{
    public interface IActorService
    {
        Task<IEnumerable<ActorDTO>> GetAllActorsAsync(GetAllActorsDTO request);
        Task<ActorDTO> GetActorAsync(int id);
        Task<ActorDTO> CreateActorAsync(CreateActorDTO request);
        Task AssignActorToMovieAsync(int movieId, int actorId);
        Task UpdateActorAsync(int id, UpdateActorDTO request);
        Task DeleteActorAsync(int id);
    }
}
