using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/[controller]/[action]")]
public class AdminDashboardController : ControllerBase
{
    private readonly IAdminDashboardService _service;

    public AdminDashboardController(IAdminDashboardService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SalesReportDTO>>> SalesReportAsync(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.SalesReportAsync(cancellationToken));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerActivityLogDTO>>> GetCustomerActivityLogs(
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.GetCustomerActivityLogsAsync(cancellationToken));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InventorySummaryDTO>>> GetInventorySummary(
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.GetInventorySummaryAsync(cancellationToken));
    }
}