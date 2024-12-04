using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public AuthController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Register([FromBody] RegisterRequestModel registerRequest, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        var token = await _identityService.CreateUserAsync(registerRequest.Email, registerRequest.Password,
            registerRequest.FirstName, registerRequest.LastName, registerRequest.Email);
        
        return Ok(token);
    }

    [HttpPost]
    public async Task<ActionResult<string>> Login([FromBody] LoginRequestModel loginRequest,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");
        
        var token = await _identityService.AuthenticateUserAsync(loginRequest.Email, loginRequest.Password);
        
        return Ok(token);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _identityService.Logout();
        return Ok();
    }
}