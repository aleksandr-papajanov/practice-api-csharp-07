using PracticeApiCSharp07.DTOs.Movies;

namespace PracticeApiCSharp07.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDTO>> GetAllMoviesAsync(GetAllMoviesDTO request);
        Task<MovieDTO> GetMovieAsync(int id);
        Task<MovieDetailsDTO> GetMovieDetailsAsync(int id);
        Task<MovieDTO> CreateMovieAsync(CreateMovieDTO request);
        Task UpdateMovieAsync(int id, UpdateMovieDTO request);
        Task DeleteMovieAsync(int id);
    }
}