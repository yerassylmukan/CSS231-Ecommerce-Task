using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CatalogItemReviewController : ControllerBase
{
    private readonly ICatalogItemReviewService _service;

    public CatalogItemReviewController(ICatalogItemReviewService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CatalogItemReviewDTO>>> GetCatalogItemReviews(
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.GetCatalogItemReviewsAsync(cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CatalogItemReviewDTO>> GetCatalogItemByIdReview(int id,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.GetCatalogItemReviewByIdAsync(id, cancellationToken));
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<CatalogItemDTO>> GetCatalogItemReviewByUserId(string userId,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.GetCatalogItemReviewByUserIdAsync(userId, cancellationToken));
    }

    [HttpGet("{itemId}")]
    public async Task<ActionResult<CatalogItemReviewDTO>> GetCatalogItemReviewByCatalogItemId(int itemId,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.GetCatalogItemReviewByCatalogItemIdAsync(itemId, cancellationToken));
    }

    [HttpPost]
    [Authorize(Roles = "Admin,BasicUser")]
    public async Task<ActionResult<CatalogItemReviewDTO>> CreateCatalogItemReview(
        [FromBody] CreateCatalogItemRevirewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.CreateCatalogItemReviewAsync(model.UserId, model.Rating, model.ReviewText,
            model.CatalogItemId, cancellationToken));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,BasicUser")]
    public async Task<IActionResult> UpdateCatalogItemReview(int id,
        [FromBody] UpdateCatalogItemRevirewModel model, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        await _service.UpdateCatalogItemReviewAsync(id, model.Rating, model.ReviewText, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,BasicUser")]
    public async Task<IActionResult> DeleteCatalogItemReview(int id,
        [FromBody] UpdateCatalogItemRevirewModel model, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        await _service.DeleteCatalogItemReviewAsync(id, cancellationToken);

        return NoContent();
    }
}