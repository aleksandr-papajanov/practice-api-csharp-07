using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using PracticeApiCSharp07.DTOs.Actors;
using PracticeApiCSharp07.DTOs.Movies;
using PracticeApiCSharp07.Services;

namespace PracticeApiCSharp07.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _service;

        public ActorController(IActorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorDTO>>> GetAll([FromQuery] GetAllActorsDTO request)
        {
            var actors = await _service.GetAllActorsAsync(request);
            return Ok(actors);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ActorDTO>> Get([FromRoute] int id)
        {
            var actor = await _service.GetActorAsync(id);
            return Ok(actor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateActorDTO request)
        {
            var actor = await _service.CreateActorAsync(request);
            return CreatedAtAction(nameof(Get), new { id = actor.Id }, actor);
        }

        [HttpPost("{movieId:int}/{actorId:int}")]
        public async Task<IActionResult> AssignActorToMovie([FromRoute] int movieId, [FromRoute] int actorId)
        {
            await _service.AssignActorToMovieAsync(movieId, actorId);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateActorDTO request)
        {
            await _service.UpdateActorAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _service.DeleteActorAsync(id);
            return NoContent();
        }
    }
}
