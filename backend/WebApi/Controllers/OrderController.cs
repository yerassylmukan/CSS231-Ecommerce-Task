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
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;

    public OrderController(IOrderService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.GetOrdersAsync(cancellationToken));
    }

    [HttpGet("{orderId}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<OrderDTO>> GetOrderById(int orderId, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.GetOrderByIdAsync(orderId, cancellationToken));
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<OrderDTO>> GetOrderByUserId(string userId, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.GetOrderByUserIdAsync(userId, cancellationToken));
    }

    [HttpPost("{userId}")]
    public async Task<ActionResult<OrderDTO>> CreateOrder(string userId, [FromBody] ShippingMethodModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (currentUserId != userId)
            throw new ArgumentException("User does not belong to this user");

        return Ok(await _service.CreateOrderAsync(userId, model.deliveryName, model.deliveryCost, model.deliveryTime,
            cancellationToken));
    }

    [HttpPost("{orderId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ConfirmOrder(int orderId, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        await _service.ConfirmOrderAsync(orderId, cancellationToken);

        return Ok();
    }
}