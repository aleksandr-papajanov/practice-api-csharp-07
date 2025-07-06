using Microsoft.AspNetCore.Mvc;
using PracticeApiCSharp07.DTOs.Reviews;
using PracticeApiCSharp07.Services;

namespace PracticeApiCSharp07.Controllers
{
    /// <summary>
    /// Controller for managing reviews for movies.
    /// </summary>
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewController"/>.
        /// </summary>
        /// <param name="service">The review service.</param>
        public ReviewController(IReviewService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a specific review by ID.
        /// </summary>
        /// <param name="id">The ID of the review.</param>
        /// <returns>The review data.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReviewDTO))]
        public async Task<ActionResult<ReviewDTO>> Get([FromRoute] int id)
        {
            var review = await _service.GetReviewAsync(id);
            return Ok(review);
        }

        /// <summary>
        /// Creates a new review for a movie.
        /// </summary>
        /// <param name="request">The review data.</param>
        /// <returns>The created review with location header.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReviewDTO))]
        public async Task<IActionResult> Create([FromBody] CreateReviewDTO request)
        {
            var review = await _service.CreateReviewAsync(request);
            return CreatedAtAction(nameof(Get), new { id = review.Id }, review);
        }

        /// <summary>
        /// Updates an existing review.
        /// </summary>
        /// <param name="id">The ID of the review to update.</param>
        /// <param name="request">The updated review data.</param>
        /// <returns>No content on success.</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateReviewDTO request)
        {
            await _service.UpdateReviewAsync(id, request);
            return NoContent();
        }

        /// <summary>
        /// Deletes a review by ID.
        /// </summary>
        /// <param name="id">The ID of the review to delete.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _service.DeleteReviewAsync(id);
            return NoContent();
        }
    }

}
