using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public string GetCurrentUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) Console.WriteLine("No user found or user is not authenticated.");
        return userId;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public string GetHello()
    {
        return "Hello";
    }
}