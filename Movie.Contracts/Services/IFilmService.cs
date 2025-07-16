using Movie.Core.DTOs.Films;

namespace Movie.Contracts.Services
{
    public interface IFilmService
    {
        Task<IEnumerable<FilmDTO>> GetAllFilmsAsync(GetAllFilmsDTO request);
        Task<FilmDTO> GetFilmAsync(int id);
        Task<FilmDetailsDTO> GetFilmDetailsAsync(int id);
        Task<FilmDTO> CreateFilmAsync(CreateFilmDTO request);
        Task UpdateFilmAsync(int id, UpdateFilmDTO request);
        Task DeleteFilmAsync(int id);
    }
}