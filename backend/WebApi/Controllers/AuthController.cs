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
    public async Task<ActionResult<string>> Login([FromBody] LoginModel login,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");
        
        var token = await _identityService.AuthenticateUserAsync(login.Email, login.Password);
        
        return Ok(token);
    }

    [HttpPost]
    public async Task<ActionResult<string>> AddUserToRoles([FromBody] AddUserToRolesModel addUserToRolesModel)
    {
        var newToken = await _identityService.AddUserToRolesAsync(addUserToRolesModel.Email, addUserToRolesModel.Roles);
        
        return Ok(newToken);
    }
    
    [HttpPost]
    public async Task<IActionResult> RequestPasswordReset([FromBody] RequestPasswordResetModel requestPasswordResetModel)
    {
        await _identityService.SendPasswordResetTokenAsync(requestPasswordResetModel.Email, requestPasswordResetModel.LinkToResetPassword);
        return Ok("Password reset link sent.");
    }

    [HttpPost()]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel resetPassword)
    {
        await _identityService.ResetPasswordAsync(resetPassword.Email, resetPassword.Token, resetPassword.NewPassword);
        return Ok("Password has been reset.");
    }
}