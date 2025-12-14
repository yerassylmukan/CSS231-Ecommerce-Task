using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using ApplicationCore.DTOs;

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
    public async Task<ActionResult<TokenDTO>> Register([FromBody] RegisterModel register,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        var token = await _identityService.CreateUserAsync(register.Email, register.Password,
            register.FirstName, register.LastName, register.ProfilePictureUrl);

        return Ok(new TokenDTO { AuthToken = token });
    }

    [HttpPost]
    public async Task<ActionResult<TokenDTO>> Login([FromBody] LoginModel login,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        var token = await _identityService.AuthenticateUserAsync(login.Email, login.Password);

        return Ok(new TokenDTO { AuthToken = token });
    }

    [HttpPost]
    public ActionResult<TokenDTO> AuthenticateAnonymousUser()
    {
        return Ok(new TokenDTO { AuthToken = _identityService.AuthenticateAnonymousUser() });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<TokenDTO>> AddUserToRoles([FromBody] AddUserToRolesModel addUserToRolesModel,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        var newToken = await _identityService.AddUserToRolesAsync(addUserToRolesModel.Email, addUserToRolesModel.Roles);

        return Ok(new TokenDTO { AuthToken = newToken });
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
    public async Task<ActionResult<TokenDTO>> ChangeEmail([FromBody] ChangeEmailModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        var result = await _identityService.ChangeEmailAsync(model.OldEmail, model.NewEmail);
        
        return Ok(new TokenDTO { AuthToken = result });
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
    public ActionResult<CurrentUserIdDTO> GetCurrentUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
            return Unauthorized("No user found or user is not authenticated.");

        return Ok(new CurrentUserIdDTO
        {
            UserId = userId
        });
    }

    [HttpGet]
    [Authorize]
    public ActionResult<CurrentUserNameDTO> GetCurrentUserName()
    {
        var userName = User.FindFirstValue(ClaimTypes.Name);

        if (string.IsNullOrEmpty(userName))
            return Unauthorized("No user found or user is not authenticated.");

        return Ok(new CurrentUserNameDTO
        {
            UserName = userName
        });
    }

    [HttpGet("{token}")]
    public ActionResult<JwtPayloadDTO> GetPayload(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return BadRequest("Token is required.");

        try
        {
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
                return BadRequest("The token is not in a valid JWT format.");

            var jwt = handler.ReadJwtToken(token);

            string Get(params string[] types) =>
                types.Select(t => jwt.Claims.FirstOrDefault(c => c.Type == t)?.Value)
                    .FirstOrDefault(v => !string.IsNullOrWhiteSpace(v))
                ?? string.Empty;

            List<string> GetMany(params string[] types) =>
                jwt.Claims.Where(c => types.Contains(c.Type)).Select(c => c.Value).ToList();

            long GetLong(string type) =>
                long.TryParse(jwt.Claims.FirstOrDefault(c => c.Type == type)?.Value, out var v) ? v : 0;

            var dto = new JwtPayloadDTO
            {
                NameId = Get("nameid", ClaimTypes.NameIdentifier),
                Name   = Get("unique_name", "name", ClaimTypes.Name),
                
                Roles  = GetMany("role", ClaimTypes.Role),

                NotBefore = GetLong(JwtRegisteredClaimNames.Nbf),
                Expires   = GetLong(JwtRegisteredClaimNames.Exp),
                IssuedAt  = GetLong(JwtRegisteredClaimNames.Iat)
            };

            return Ok(dto);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}