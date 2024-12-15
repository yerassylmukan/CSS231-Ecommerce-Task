using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/[controller]/[action]")]
public class CatalogTypeController : ControllerBase
{
    private readonly ICatalogTypeService _service;

    public CatalogTypeController(ICatalogTypeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CatalogTypeDTO>>> GetCatalogTypes(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.GetCatalogTypesAsync(cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CatalogTypeDTO>> GetCatalogTypeById(int id, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.GetCatalogTypeByIdAsync(id, cancellationToken));
    }

    [HttpGet("{typeName}")]
    public async Task<ActionResult<CatalogTypeDTO>> GetCatalogTypeByName(string typeName,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.GetCatalogTypeByNameAsync(typeName, cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult<CatalogTypeDTO>> CreateCatalogType([FromBody] CreateCatalogTypeModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.CreateCatalogTypeAsync(model.TypeName, cancellationToken));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCatalogType(int id, [FromBody] UpdateCatalogTypeModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        await _service.UpdateCatalogTypeAsync(id, model.TypeName, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCatalogType(int id, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        await _service.DeleteCatalogTypeAsync(id, cancellationToken);

        return NoContent();
    }
}