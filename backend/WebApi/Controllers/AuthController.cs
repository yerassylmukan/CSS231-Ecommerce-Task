using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
            register.FirstName, register.LastName, register.ProfilePictureUrl);

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
    [Authorize(Roles = "Admin")]
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

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel resetPassword,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        await _identityService.ResetPasswordAsync(resetPassword.Email, resetPassword.Code, resetPassword.NewPassword);

        return Ok();
    }

    [HttpPost]
    [Authorize(Roles = "Admin,BasicUser")]
    public async Task<ActionResult<string>> ChangeEmail([FromBody] ChangeEmailModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        var result = await _identityService.ChangeEmailAsync(model.OldEmail, model.NewEmail);

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,BasicUser")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        await _identityService.ChangePasswordAsync(model.Email, model.OldPassword, model.NewPassword);

        return Ok();
    }

    [HttpGet]
    [Authorize]
    public string GetCurrentUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) Console.WriteLine("No user found or user is not authenticated.");
        return userId;
    }

    [HttpGet]
    [Authorize]
    public string GetCurrentUserName()
    {
        var userName = User.FindFirstValue(ClaimTypes.Name);
        if (string.IsNullOrEmpty(userName)) Console.WriteLine("No user found or user is not authenticated.");
        return userName;
    }

    [HttpGet("{token}")]
    public IActionResult GetPayload(string token)
    {
        if (string.IsNullOrEmpty(token)) return BadRequest("Token is required.");

        try
        {
            var handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(token)) return BadRequest("The token is not in a valid JWT format.");

            var jwtToken = handler.ReadJwtToken(token);

            var payload = jwtToken.Claims
                .ToDictionary(claim => claim.Type, claim => (object)claim.Value);

            return Ok(payload);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}