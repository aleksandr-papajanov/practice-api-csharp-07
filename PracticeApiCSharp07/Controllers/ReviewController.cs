using Microsoft.AspNetCore.Mvc;
using PracticeApiCSharp07.DTOs.Reviews;
using PracticeApiCSharp07.Services;

namespace PracticeApiCSharp07.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewController(IReviewService service)
        {
            _service = service;
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<ReviewDTO>> Get([FromRoute] int id)
        {
            var review = await _service.GetReviewAsync(id);
            return Ok(review);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReviewDTO request)
        {
            var review = await _service.CreateReviewAsync(request);
            return CreatedAtAction(nameof(Get), new { id = review.Id }, review);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateReviewDTO request)
        {
            await _service.UpdateReviewAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _service.DeleteReviewAsync(id);
            return NoContent();
        }
    }
}
