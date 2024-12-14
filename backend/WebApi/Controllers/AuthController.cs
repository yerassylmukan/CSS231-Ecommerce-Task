using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<ActionResult<string>> Register([FromBody] RegisterModel register,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        var token = await _identityService.CreateUserAsync(register.Email, register.Password,
            register.FirstName, register.LastName, register.Email);

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
    public ActionResult<string> AuthenticateAnonymousUser()
    {
        return Ok(_identityService.AuthenticateAnonymousUser());
    }

    [HttpPost]
    [Authorize(Roles = "Admin,UserManager")]
    public async Task<ActionResult<string>> AddUserToRoles([FromBody] AddUserToRolesModel addUserToRolesModel,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        var newToken = await _identityService.AddUserToRolesAsync(addUserToRolesModel.Email, addUserToRolesModel.Roles);

        return Ok(newToken);
    }

    [HttpPost]
    public async Task<IActionResult> RequestPasswordReset(
        [FromBody] RequestPasswordResetModel requestPasswordResetModel, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        await _identityService.SendPasswordResetTokenAsync(requestPasswordResetModel.Email,
            requestPasswordResetModel.LinkToResetPassword);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel resetPassword,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        await _identityService.ResetPasswordAsync(resetPassword.Email, resetPassword.Token, resetPassword.NewPassword);

        return NoContent();
    }
}