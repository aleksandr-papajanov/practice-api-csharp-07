using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Contracts;
using Movie.Core.DTOs;
using Movie.Core.DTOs.Films;

namespace Movie.API.Controllers
{
    /// <summary>
    /// Controller for managing movies and retrieving movie details.
    /// </summary>
    [Route("api/movies")]
    [ApiController]
    [Produces("application/json")]
    public class FilmController : ControllerBase
    {
        private readonly IFilmService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilmController"/>.
        /// </summary>
        public FilmController(IFilmService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a paginated list of movies with optional filters.
        /// </summary>
        /// <param name="request">The filter and pagination parameters.</param>
        /// <returns>A list of movies.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FilmDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        public async Task<ActionResult<IEnumerable<FilmDTO>>> GetAll([FromQuery] GetAllFilmsDTO request)
        {
            var result = await _service.GetAllFilmsAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a specific movie by ID.
        /// </summary>
        /// <param name="id">The ID of the movie.</param>
        /// <returns>The movie data.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FilmDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        public async Task<ActionResult<FilmDTO>> Get([FromRoute] int id)
        {
            var movie = await _service.GetFilmAsync(id);
            return Ok(movie);
        }

        /// <summary>
        /// Retrieves detailed information about a movie, including actors and reviews.
        /// </summary>
        /// <param name="id">The ID of the movie.</param>
        /// <returns>The detailed movie data.</returns>
        [HttpGet("{id:int}/details")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FilmDetailsDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        public async Task<ActionResult<FilmDetailsDTO>> GetDetails([FromRoute] int id)
        {
            var movie = await _service.GetFilmDetailsAsync(id);
            return Ok(movie);
        }

        /// <summary>
        /// Creates a new movie.
        /// </summary>
        /// <param name="request">The movie data.</param>
        /// <returns>The created movie with location header.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FilmDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> Create([FromBody] CreateFilmDTO request)
        {
            var movie = await _service.CreateFilmAsync(request);
            return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
        }

        /// <summary>
        /// Updates an existing movie.
        /// </summary>
        /// <param name="id">The ID of the movie to update.</param>
        /// <param name="request">The updated movie data.</param>
        /// <returns>No content on success.</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateFilmDTO request)
        {
            await _service.UpdateFilmAsync(id, request);
            return NoContent();
        }

        /// <summary>
        /// Deletes a movie by ID.
        /// </summary>
        /// <param name="id">The ID of the movie to delete.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _service.DeleteFilmAsync(id);
            return NoContent();
        }
    }

}
