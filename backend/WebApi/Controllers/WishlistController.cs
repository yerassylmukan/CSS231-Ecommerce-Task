using System.Security.Claims;
using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class WishlistController : ControllerBase
{
    private readonly IWishlistService _service;

    public WishlistController(IWishlistService service)
    {
        _service = service;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<WishlistDTO>> GetWishlist(string userId, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (currentUserId != userId)
            throw new ArgumentException("User does not belong to this user");

        return Ok(await _service.GetWishlistAsync(userId, cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult<WishlistDTO>> AddItemToWishlist(string userId,
        [FromBody] AddItemToWishlistModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (currentUserId != userId)
            throw new ArgumentException("User does not belong to this user");

        return Ok(await _service.AddItemToWishlistAsync(userId, model.CatalogItemId, cancellationToken));
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> RemoveItemFromWishlist(string userId, [FromBody] RemoveItemFromWishlistModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (currentUserId != userId)
            throw new ArgumentException("User does not belong to this user");

        await _service.RemoveItemFromWishlistAsync(userId, model.CatalogItemId, cancellationToken);

        return Ok();
    }

    [HttpDelete("{wishlistId}")]
    public async Task<IActionResult> RemoveWishlistAsync(int wishlistId, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        await _service.RemoveWishlistAsync(wishlistId, cancellationToken);

        return Ok();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> RemoveWishlistByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (currentUserId != userId)
            throw new ArgumentException("User does not belong to this user");

        await _service.RemoveWishlistByUserIdAsync(userId, cancellationToken);

        return Ok();
    }
}