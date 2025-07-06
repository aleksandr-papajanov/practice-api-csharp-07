using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeApiCSharp07.DTOs.Actors;
using PracticeApiCSharp07.DTOs.Movies;
using PracticeApiCSharp07.Entities;
using PracticeApiCSharp07.Infrastructure;
using PracticeApiCSharp07.Services;

namespace PracticeApiCSharp07.Controllers
{
    /// <summary>
    /// Controller for managing movies and retrieving movie details.
    /// </summary>
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieController"/>.
        /// </summary>
        public MovieController(IMovieService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a paginated list of movies with optional filters.
        /// </summary>
        /// <param name="request">The filter and pagination parameters.</param>
        /// <returns>A list of movies.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MovieDTO>))]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetAll([FromQuery] GetAllMoviesDTO request)
        {
            var result = await _service.GetAllMoviesAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a specific movie by ID.
        /// </summary>
        /// <param name="id">The ID of the movie.</param>
        /// <returns>The movie data.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDTO))]
        public async Task<ActionResult<MovieDTO>> Get([FromRoute] int id)
        {
            var movie = await _service.GetMovieAsync(id);
            return Ok(movie);
        }

        /// <summary>
        /// Retrieves detailed information about a movie, including actors and reviews.
        /// </summary>
        /// <param name="id">The ID of the movie.</param>
        /// <returns>The detailed movie data.</returns>
        [HttpGet("{id:int}/details")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDetailsDTO))]
        public async Task<ActionResult<MovieDetailsDTO>> GetDetails([FromRoute] int id)
        {
            var movie = await _service.GetMovieDetailsAsync(id);
            return Ok(movie);
        }

        /// <summary>
        /// Creates a new movie.
        /// </summary>
        /// <param name="request">The movie data.</param>
        /// <returns>The created movie with location header.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MovieDTO))]
        public async Task<IActionResult> Create([FromBody] CreateMovieDTO request)
        {
            var movie = await _service.CreateMovieAsync(request);
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
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateMovieDTO request)
        {
            await _service.UpdateMovieAsync(id, request);
            return NoContent();
        }

        /// <summary>
        /// Deletes a movie by ID.
        /// </summary>
        /// <param name="id">The ID of the movie to delete.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _service.DeleteMovieAsync(id);
            return NoContent();
        }
    }

}
