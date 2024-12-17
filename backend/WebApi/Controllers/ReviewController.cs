using System.Security.Claims;
using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _service;

    public ReviewController(IReviewService service)
    {
        _service = service;
    }

    [HttpGet("{catalogItemId}")]
    public async Task<ActionResult<IEnumerable<CatalogItemReviewDTO>>> GetCatalogItemReview(int catalogItemId,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.GetReviewsAsync(catalogItemId, cancellationToken));
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<IEnumerable<CatalogItemReviewDTO>>> GetReviewsByUserId(string userId,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.GetReviewsByUserIdAsync(userId, cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<CatalogItemReviewDTO>>> GetReviewsById(int id,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.GetReviewByIdAsync(id, cancellationToken));
    }

    [HttpPost]
    [Authorize(Roles = "Admin,BasicUser")]
    public async Task<ActionResult<CatalogItemReviewDTO>> CreateReview([FromBody] CreateReviewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (currentUserId != model.UserId)
            throw new ArgumentException("User does not belong to this user");

        return Ok(await _service.CreateReviewAsync(model.UserId, model.Rating, model.ReviewText, model.CatalogItemId,
            cancellationToken));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,BasicUser")]
    public async Task<IActionResult> UpdateReview(int id, [FromBody] UpdateReviewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (currentUserId != model.UserId)
            throw new ArgumentException("User does not belong to this user");

        await _service.UpdateReviewAsync(id, model.UserId, model.Rating, model.ReviewText!, cancellationToken);

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,BasicUser")]
    public async Task<IActionResult> DeleteReviewAsync(int id, [FromBody] DeleteReviewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        await _service.DeleteReviewAsync(id, model.UserId, cancellationToken);

        return Ok();
    }
}