using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class SupportController : ControllerBase
{
    private readonly IEmailSender _emailSender;

    public SupportController(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    [HttpPost]
    public async Task<IActionResult> SendSupportAsync([FromBody] SupportRequestModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        await _emailSender.SendSupportAsync(model.FistName, model.LastName,  model.Subject, model.Message, cancellationToken);

        return Ok();
    }
}