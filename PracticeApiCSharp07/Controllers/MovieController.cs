using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeApiCSharp07.DTOs.Movies;
using PracticeApiCSharp07.Entities;
using PracticeApiCSharp07.Infrastructure;
using PracticeApiCSharp07.Services;

namespace PracticeApiCSharp07.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _service;

        public MovieController(IMovieService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetAll([FromQuery] GetAllMoviesDTO request)
        {
            var result = await _service.GetAllMoviesAsync(request);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDTO>> Get([FromRoute] int id)
        {
            var movie = await _service.GetMovieAsync(id);
            return Ok(movie);
        }
        
        [HttpGet("{id:int}/details")]
        public async Task<ActionResult<MovieDetailsDTO>> GetDetails([FromRoute] int id)
        {
            var movie = await _service.GetMovieDetailsAsync(id);
            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMovieDTO request)
        {
            var movie = await _service.CreateMovieAsync(request);
            return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateMovieDTO request)
        {
            await _service.UpdateMovieAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _service.DeleteMovieAsync(id);
            return NoContent();
        }
    }
}
