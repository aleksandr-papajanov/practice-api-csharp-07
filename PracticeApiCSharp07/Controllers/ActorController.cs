using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using PracticeApiCSharp07.DTOs.Actors;
using PracticeApiCSharp07.DTOs.Movies;
using PracticeApiCSharp07.Services;

namespace PracticeApiCSharp07.Controllers
{
    /// <summary>
    /// Controller for managing actors and assigning them to movies.
    /// </summary>
    [Route("api/actors")]
    [ApiController]
    [Produces("application/json")]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorController"/>.
        /// </summary>
        public ActorController(IActorService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a paginated list of all actors.
        /// </summary>
        /// <param name="request">The pagination parameters.</param>
        /// <returns>A list of actors.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ActorDTO>))]
        public async Task<ActionResult<IEnumerable<ActorDTO>>> GetAll([FromQuery] GetAllActorsDTO request)
        {
            var actors = await _service.GetAllActorsAsync(request);
            return Ok(actors);
        }

        /// <summary>
        /// Retrieves an actor by ID.
        /// </summary>
        /// <param name="id">The ID of the actor.</param>
        /// <returns>The requested actor.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActorDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ActorDTO>> Get([FromRoute] int id)
        {
            var actor = await _service.GetActorAsync(id);
            return Ok(actor);
        }

        /// <summary>
        /// Creates a new actor.
        /// </summary>
        /// <param name="request">The actor data.</param>
        /// <returns>The created actor with location header.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ActorDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] CreateActorDTO request)
        {
            var actor = await _service.CreateActorAsync(request);
            return CreatedAtAction(nameof(Get), new { id = actor.Id }, actor);
        }

        /// <summary>
        /// Assigns an actor to a movie.
        /// </summary>
        /// <param name="movieId">The ID of the movie.</param>
        /// <param name="actorId">The ID of the actor.</param>
        /// <returns>No content on success.</returns>
        [HttpPost("{movieId:int}/{actorId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AssignActorToMovie([FromRoute] int movieId, [FromRoute] int actorId)
        {
            await _service.AssignActorToMovieAsync(movieId, actorId);
            return NoContent();
        }

        /// <summary>
        /// Updates an existing actor.
        /// </summary>
        /// <param name="id">The ID of the actor to update.</param>
        /// <param name="request">The updated actor data.</param>
        /// <returns>No content on success.</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateActorDTO request)
        {
            await _service.UpdateActorAsync(id, request);
            return NoContent();
        }

        /// <summary>
        /// Deletes an actor by ID.
        /// </summary>
        /// <param name="id">The ID of the actor to delete.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _service.DeleteActorAsync(id);
            return NoContent();
        }
    }

}
