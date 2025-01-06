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
public class CartController : ControllerBase
{
    private readonly ICartService _service;

    public CartController(ICartService service)
    {
        _service = service;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<CartDTO>> GetCart(string userId, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (currentUserId != userId)
            throw new ArgumentException("User does not belong to this user");

        return Ok(await _service.GetCartAsync(userId, cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult<CartDTO>> AddItemToCart(string userId, [FromBody] AddItemToCartModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (currentUserId != userId)
            throw new ArgumentException("User does not belong to this user");

        return Ok(await _service.AddItemToCartAsync(userId, model.CatalogItemId, cancellationToken));
    }

    [HttpPut("{userId}")]
    public async Task<ActionResult<CartDTO>> UpdateItemQuantity(string userId,
        [FromBody] UpdateCartItemQuantityModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (currentUserId != userId)
            throw new ArgumentException("User does not belong to this user");

        return Ok(
            await _service.UpdateItemQuantityAsync(userId, model.CatalogItemId, model.Quantity, cancellationToken));
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> RemoveItemFromCart(string userId, [FromBody] RemoveItemFromCartModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (currentUserId != userId)
            throw new ArgumentException("User does not belong to this user");

        await _service.RemoveItemFromCartAsync(userId, model.CatalogItemId, cancellationToken);

        return Ok();
    }

    [HttpDelete("{cartId}")]
    public async Task<IActionResult> RemoveCartAsync(int cartId, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        await _service.RemoveCartAsync(cartId, cancellationToken);

        return Ok();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> RemoveCartByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (currentUserId != userId)
            throw new ArgumentException("User does not belong to this user");

        await _service.RemoveCartByUserIdAsync(userId, cancellationToken);

        return Ok();
    }
}