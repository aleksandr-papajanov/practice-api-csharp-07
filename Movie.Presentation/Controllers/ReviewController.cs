using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Contracts;
using Movie.Core.DTOs;
using Movie.Core.DTOs.Reviews;

namespace Movie.Presentation.Controllers
{
    /// <summary>
    /// Controller for managing reviews for movies.
    /// </summary>
    [Route("api/reviews")]
    [ApiController]
    [Produces("application/json")]
    public class ReviewController : ControllerBase
    {
        private readonly IServiceManager _manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewController"/>.
        /// </summary>
        public ReviewController(IServiceManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Retrieves a specific review by ID.
        /// </summary>
        /// <param name="id">The ID of the review.</param>
        /// <returns>The review data.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReviewDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ReviewDTO>> Get([FromRoute] int id)
        {
            var review = await _manager.ReviewService.GetReviewAsync(id);
            return Ok(review);
        }

        /// <summary>
        /// Creates a new review for a movie.
        /// </summary>
        /// <param name="request">The review data.</param>
        /// <returns>The created review with location header.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReviewDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Create([FromBody] CreateReviewDTO request)
        {
            var review = await _manager.ReviewService.CreateReviewAsync(request);
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
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateReviewDTO request)
        {
            await _manager.ReviewService.UpdateReviewAsync(id, request);
            return NoContent();
        }

        /// <summary>
        /// Deletes a review by ID.
        /// </summary>
        /// <param name="id">The ID of the review to delete.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _manager.ReviewService.DeleteReviewAsync(id);
            return NoContent();
        }
    }

}
