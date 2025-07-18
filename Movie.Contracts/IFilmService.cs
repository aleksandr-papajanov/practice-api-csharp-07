using Microsoft.AspNetCore.JsonPatch;
using Movie.Core.DTOs.Common;
using Movie.Core.DTOs.Films;

namespace Movie.Contracts
{
    public interface IFilmService
    {
        Task<PaginatedResult<FilmDTO>> GetAllFilmsAsync(GetAllFilmsDTO request);
        Task<FilmDTO> GetFilmAsync(int id);
        Task<FilmDetailsDTO> GetFilmDetailsAsync(int id);
        Task<FilmDTO> CreateFilmAsync(CreateFilmDTO request);
        Task UpdateFilmAsync(int id, UpdateFilmDTO request);
        Task DeleteFilmAsync(int id);
        Task UpdateFilmWithPatchDocumentAsync(int id, JsonPatchDocument<UpdateFilmDTO> patchDocument);
    }
}