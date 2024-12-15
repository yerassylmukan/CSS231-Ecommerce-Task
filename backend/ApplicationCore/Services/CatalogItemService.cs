using ApplicationCore.CustomMappers;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services;

public class CatalogItemService : ICatalogItemService
{
    private readonly IApplicationDbContext _context;
    private readonly IEmailSender _emailSender;

    public CatalogItemService(IApplicationDbContext context, IEmailSender emailSender)
    {
        _context = context;
        _emailSender = emailSender;
    }

    public async Task<IEnumerable<CatalogItemDTO>> GetCatalogItemsAsync(CancellationToken cancellationToken)
    {
        var item = await _context.CatalogItems.Include(ci => ci.Reviews).ToListAsync(cancellationToken);

        var result = item.Select(ci => ci.MapToDTO());

        return result;
    }

    public async Task<CatalogItemDTO> GetCatalogItemByIdAsync(int id, CancellationToken cancellationToken)
    {
        var item = await _context.CatalogItems.Include(ci => ci.Reviews)
            .FirstOrDefaultAsync(ci => ci.Id == id, cancellationToken);

        if (item == null) throw new CatalogItemDoesNotExistsException(id);

        return item.MapToDTO();
    }

    public async Task<CatalogItemDTO> GetCatalogItemsByTypeNameAsync(string catalogTypeName,
        CancellationToken cancellationToken)
    {
        var catalogType =
            await _context.CatalogTypes.FirstOrDefaultAsync(cb => cb.Type == catalogTypeName, cancellationToken);

        if (catalogType == null)
            throw new CatalogTypeDoesNotExistsException(catalogTypeName);

        var item = await _context.CatalogItems.Include(ci => ci.Reviews).FirstOrDefaultAsync(
            ci => ci.CatalogType.Type == catalogTypeName,
            cancellationToken);

        if (item == null) throw new CatalogItemDoesNotExistsException(catalogTypeName);

        return item.MapToDTO();
    }

    public async Task<CatalogItemDTO> GetCatalogItemsByBrandNameAsync(string catalogBrandName,
        CancellationToken cancellationToken)
    {
        var brandExists =
            await _context.CatalogBrands.FirstOrDefaultAsync(cb => cb.Brand == catalogBrandName, cancellationToken);

        if (brandExists == null) throw new CatalogBrandDoesNotExistsException(catalogBrandName);

        var item = await _context.CatalogItems.Include(ci => ci.Reviews).FirstOrDefaultAsync(
            ci => ci.CatalogBrand.Brand == catalogBrandName,
            cancellationToken);

        if (item == null) throw new CatalogItemDoesNotExistsException(catalogBrandName);

        return item.MapToDTO();
    }

    public async Task<CatalogItemDTO> CreateCatalogItemAsync(string name, string description, decimal price,
        string pictureUrl, int stockQuantity,
        int catalogTypeId, int catalogBrandId, CancellationToken cancellationToken)
    {
        var item = new CatalogItem
        {
            Name = name,
            Description = description,
            Price = price,
            PictureUrl = pictureUrl,
            StockQuantity = stockQuantity,
            CatalogTypeId = catalogTypeId,
            CatalogBrandId = catalogBrandId
        };

        _context.CatalogItems.Add(item);
        await _context.SaveChangesAsync(cancellationToken);

        return item.MapToDTO();
    }

    public async Task UpdateCatalogItemDetailsAsync(int id, string name, string description, decimal price,
        CancellationToken cancellationToken)
    {
        var item = await _context.CatalogItems.FirstOrDefaultAsync(ci => ci.Id == id, cancellationToken);

        if (item == null) throw new CatalogItemDoesNotExistsException(id);

        var isChanged = false;

        if (!string.IsNullOrEmpty(name) && item.Name != name)
        {
            item.Name = name;
            isChanged = true;
        }

        if (!string.IsNullOrEmpty(description) && item.Description != description)
        {
            item.Description = description;
            isChanged = true;
        }

        if (item.Price != price)
        {
            item.Price = price;
            isChanged = true;
        }

        if (isChanged) await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateCatalogItemStockQuantityAsync(int id, int stockQuantity,
        CancellationToken cancellationToken)
    {
        var item = await _context.CatalogItems.FirstOrDefaultAsync(ci => ci.Id == id, cancellationToken);

        if (item == null) throw new CatalogItemDoesNotExistsException(id);

        if (item.StockQuantity != stockQuantity)
        {
            item.StockQuantity = stockQuantity;
            await _context.SaveChangesAsync(cancellationToken);
        }

        if (item.StockQuantity < 1)
            await _emailSender.EmailSendAsync("admin@gmail.com", $"Running out of product - {item.Name}",
                $"I would like to inform you that the product with ID {item.Id} is running out.", cancellationToken);
    }

    public async Task UpdateCatalogItemPictureUrlAsync(int id, string pictureUrl, CancellationToken cancellationToken)
    {
        var item = await _context.CatalogItems.FirstOrDefaultAsync(ci => ci.Id == id, cancellationToken);

        if (item == null) throw new CatalogItemDoesNotExistsException(id);

        if (!string.IsNullOrEmpty(pictureUrl) && item.PictureUrl != pictureUrl)
        {
            item.PictureUrl = pictureUrl;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task UpdateCatalogTypeAsync(int id, int catalogTypeId, CancellationToken cancellationToken)
    {
        var item = await _context.CatalogItems.FirstOrDefaultAsync(ci => ci.Id == id, cancellationToken);

        if (item == null) throw new CatalogItemDoesNotExistsException(id);

        var type = await _context.CatalogTypes.FirstOrDefaultAsync(cb => cb.Id == catalogTypeId, cancellationToken);

        if (type == null) throw new CatalogTypeDoesNotExistsException(catalogTypeId);

        if (catalogTypeId != item.CatalogTypeId)
        {
            item.CatalogTypeId = catalogTypeId;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task UpdateCatalogBrandAsync(int id, int catalogBrandId, CancellationToken cancellationToken)
    {
        var item = await _context.CatalogItems.FirstOrDefaultAsync(ci => ci.Id == id, cancellationToken);

        if (item == null) throw new CatalogItemDoesNotExistsException(id);

        var brand = await _context.CatalogBrands.FirstOrDefaultAsync(cb => cb.Id == catalogBrandId, cancellationToken);

        if (brand == null) throw new CatalogBrandDoesNotExistsException(catalogBrandId);

        if (catalogBrandId != item.CatalogBrandId)
        {
            item.CatalogBrandId = catalogBrandId;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteCatalogItemAsync(int id, CancellationToken cancellationToken)
    {
        var item = await _context.CatalogItems.FirstOrDefaultAsync(ci => ci.Id == id, cancellationToken);

        if (item == null) throw new CatalogItemDoesNotExistsException(id);

        _context.CatalogItems.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
    }
}