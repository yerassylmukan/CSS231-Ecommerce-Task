using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
public class ApplicationUserController : ControllerBase
{
    private readonly IApplicationUserService _service;

    public ApplicationUserController(IApplicationUserService service)
    {
        _service = service;
    }

    [HttpGet("{userName}")]
    public async Task<ActionResult<ApplicationUserDTO>> GetUserDetailsByUserName(string userName,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        var (userId, firstName, lastName, applicationUserName, email, profilePictureUrl, roles) =
            await _service.GetUserDetailsByUserNameAsync(userName);

        var result = new ApplicationUserDTO
        {
            UserId = userId,
            FirstName = firstName,
            LastName = lastName,
            UserName = applicationUserName,
            Email = email,
            ProfilePictureUrl = profilePictureUrl,
            Roles = roles
        };

        return Ok(result);
    }

    [HttpGet("{email}")]
    public async Task<ActionResult<ApplicationUserDTO>> GetUserDetailsByEmail(string email,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        var (userId, firstName, lastName, userName, applicationEmail, profilePictureUrl, roles) =
            await _service.GetUserDetailsByEmailAsync(email);

        var result = new ApplicationUserDTO
        {
            UserId = userId,
            FirstName = firstName,
            LastName = lastName,
            UserName = userName,
            Email = applicationEmail,
            ProfilePictureUrl = profilePictureUrl,
            Roles = roles
        };

        return Ok(result);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<ApplicationUserDTO>> GetUserDetailsByUserId(string userId,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        var (applicationUserId, firstName, lastName, userName, email, profilePictureUrl, roles) =
            await _service.GetUserDetailsByUserIdAsync(userId);

        var result = new ApplicationUserDTO
        {
            UserId = applicationUserId,
            FirstName = firstName,
            LastName = lastName,
            UserName = userName,
            Email = email,
            ProfilePictureUrl = profilePictureUrl,
            Roles = roles
        };

        return Ok(result);
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> UpdateProfileInformation(string userId,
        [FromBody] UpdateProfileInformationModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        await _service.UpdateProfileInformationAsync(userId, model.FirstName, model.LastName, model.Email,
            model.ProfilePictureUrl);

        return Ok();
    }
}