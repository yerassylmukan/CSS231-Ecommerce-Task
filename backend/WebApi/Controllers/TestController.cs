using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TestController : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Anonymous")]
    public ActionResult<string> Get()
    {
        return Ok("Hello World!");
    }
}