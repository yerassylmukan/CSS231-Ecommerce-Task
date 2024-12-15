using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/[controller]/[action]")]
public class CatalogBrandController : ControllerBase
{
    private readonly ICatalogBrandService _service;

    public CatalogBrandController(ICatalogBrandService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CatalogBrandDTO>>> GetCatalogBrands(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.GetCatalogBrandsAsync(cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CatalogBrandDTO>> GetCatalogBrandById(int id, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.GetCatalogBrandByIdAsync(id, cancellationToken));
    }

    [HttpGet("{brandName}")]
    public async Task<ActionResult<CatalogBrandDTO>> GetCatalogBrandByName(string brandName,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.GetCatalogBrandByNameAsync(brandName, cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult<CatalogBrandDTO>> CreateCatalogBrand([FromBody] CreateCatalogBrandModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.CreateCatalogBrandAsync(model.BandName, cancellationToken));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCatalogBrand(int id, [FromBody] UpdateCatalogBrandModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        await _service.UpdateCatalogBrandAsync(id, model.BandName, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCatalogBrand(int id, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        await _service.DeleteCatalogBrandAsync(id, cancellationToken);

        return NoContent();
    }
}