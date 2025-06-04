using business;
using domain.DTOs.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ArtMarketplace.Controllers;
[Route("review")]
public class ReviewController(IReviewService reviewService) : ControllerBase
{

    [HttpPatch()]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> SetReview([FromBody] ReviewUpdateDto reviewSetDto)
    {
        await reviewService.UpdateReview(reviewSetDto);
        return NoContent();
    }
    [HttpPost()]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> AddReview([FromBody] ReviewCreateDto reviewSetDto)
    {
        Guid idReview = await reviewService.CreateReview(reviewSetDto);
        return CreatedAtAction("AddReview", idReview);
    }
    [HttpPatch("response")]
    [Authorize(Roles = "Artisan")]
    public async Task<IActionResult> RespondToReview([FromBody] ReviewResponseDto responseDto)
    {
        await reviewService.RespondToReview(responseDto);
        return NoContent();
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Artisan, Customer")]
    public async Task<IActionResult> GetReview( Guid id)
    {
        return Ok(await reviewService.GetReview(id));
    }
}
