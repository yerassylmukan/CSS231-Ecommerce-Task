using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CatalogItemController : ControllerBase
{
    private readonly ICatalogItemService _service;

    public CatalogItemController(ICatalogItemService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CatalogItemDTO>>> GetCatalogItems(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.GetCatalogItemsAsync(cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CatalogItemDTO>> GetCatalogItemById(int id, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.GetCatalogItemByIdAsync(id, cancellationToken));
    }

    [HttpGet("{typeName}")]
    public async Task<ActionResult<CatalogItemDTO>> GetCatalogItemsByTypeName(string typeName,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.GetCatalogItemsByTypeNameAsync(typeName, cancellationToken));
    }

    [HttpGet("{brandName}")]
    public async Task<ActionResult<CatalogItemDTO>> GetCatalogItemsByBrandName(string brandName,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.GetCatalogItemsByBrandNameAsync(brandName, cancellationToken));
    }

    [HttpPost]
    [Authorize(Roles = "Admin,ProductManager")]
    public async Task<ActionResult<CatalogItemDTO>> CreateCatalogItem([FromBody] CreateCatalogItemModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        return Ok(await _service.CreateCatalogItemAsync(model.Name, model.Description, model.Price, model.PictureUrl,
            model.StockQuantity, model.CatalogTypeId, model.CatalogBrandId, cancellationToken));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,ProductManager")]
    public async Task<IActionResult> UpdateCatalogItemDetails(int id, [FromBody] UpdateCatalogItemModel model,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        await _service.UpdateCatalogItemDetailsAsync(id, model.Name!, model.Description!, model.Price,
            cancellationToken);

        return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,ProductManager")]
    public async Task<IActionResult> UpdateCatalogItemStockQuantity(int id,
        [FromBody] int stockQuantity, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        await _service.UpdateCatalogItemStockQuantityAsync(id, stockQuantity, cancellationToken);

        return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,ProductManager")]
    public async Task<IActionResult> UpdateCatalogItemPictureUrl(int id, [FromBody] string pictureUrl,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        await _service.UpdateCatalogItemPictureUrlAsync(id, pictureUrl, cancellationToken);

        return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,ProductManager")]
    public async Task<IActionResult> UpdateCatalogType(int id, [FromBody] int catalogTypeId,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        await _service.UpdateCatalogTypeAsync(id, catalogTypeId, cancellationToken);

        return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,ProductManager")]
    public async Task<IActionResult> UpdateCatalogBrand(int id, [FromBody] int catalogBrandId,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        await _service.UpdateCatalogBrandAsync(id, catalogBrandId, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,ProductManager")]
    public async Task<IActionResult> DeleteCatalogItem(int id, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled by client");

        await _service.DeleteCatalogItemAsync(id, cancellationToken);

        return NoContent();
    }
}