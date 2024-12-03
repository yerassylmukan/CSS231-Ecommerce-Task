using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class EmailSenderController : Controller
{
    private readonly IEmailSender _emailSender;

    public EmailSenderController(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    [HttpPost]
    public async Task<IActionResult> SendEmailAsync(string email, string subject, string message, CancellationToken cancellationToken)
    {
        await _emailSender.EmailSendAsync(email, subject, message, cancellationToken);
        return Ok();
    }
}