using ApplicationCore.CustomMappers;
using ApplicationCore.DTOs;
using ApplicationCore.Entities.CatalogAggregate;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services;

public class CatalogBrandService : ICatalogBrandService
{
    private readonly IApplicationDbContext _context;

    public CatalogBrandService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CatalogBrandDTO>> GetCatalogBrandsAsync(CancellationToken cancellationToken)
    {
        var catalogBrands = await _context.CatalogBrands.ToListAsync(cancellationToken);

        var result = catalogBrands.Select(cb => cb.MapToDTO());

        return result;
    }

    public async Task<CatalogBrandDTO> GetCatalogBrandByIdAsync(int id, CancellationToken cancellationToken)
    {
        var catalogBrand = await _context.CatalogBrands.FirstOrDefaultAsync(cb => cb.Id == id, cancellationToken);

        if (catalogBrand == null)
            throw new CatalogBrandDoesNotExistsException(id);

        return catalogBrand.MapToDTO();
    }

    public async Task<CatalogBrandDTO> GetCatalogBrandByNameAsync(string catalogBrandName,
        CancellationToken cancellationToken)
    {
        var catalogBrand =
            await _context.CatalogBrands.FirstOrDefaultAsync(cb => cb.Brand == catalogBrandName, cancellationToken);

        if (catalogBrand == null)
            throw new CatalogBrandDoesNotExistsException(catalogBrandName);

        return catalogBrand.MapToDTO();
    }

    public async Task<CatalogBrandDTO> CreateCatalogBrandAsync(string catalogBrandName,
        CancellationToken cancellationToken)
    {
        var brandExists =
            await _context.CatalogBrands.FirstOrDefaultAsync(cb => cb.Brand == catalogBrandName, cancellationToken);

        if (brandExists != null) throw new CatalogBrandAlreadyExistsException(catalogBrandName);

        var catalogBrand = new CatalogBrand
        {
            Brand = catalogBrandName
        };

        var result = _context.CatalogBrands.Add(catalogBrand);
        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity.MapToDTO();
    }

    public async Task UpdateCatalogBrandAsync(int id, string catalogBrandName, CancellationToken cancellationToken)
    {
        var catalogBrand = await _context.CatalogBrands.FirstOrDefaultAsync(cb => cb.Id == id, cancellationToken);

        if (catalogBrand == null)
            throw new CatalogBrandDoesNotExistsException(id);

        if (string.IsNullOrEmpty(catalogBrandName))
            throw new ArgumentException($"'{nameof(catalogBrandName)}' cannot be null or empty.");

        if (catalogBrandName != catalogBrand.Brand)
        {
            catalogBrand.Brand = catalogBrandName;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteCatalogBrandAsync(int id, CancellationToken cancellationToken)
    {
        var catalogBrand = await _context.CatalogBrands.FirstOrDefaultAsync(cb => cb.Id == id, cancellationToken);

        if (catalogBrand == null)
            throw new CatalogBrandDoesNotExistsException(id);

        _context.CatalogBrands.Remove(catalogBrand);
        await _context.SaveChangesAsync(cancellationToken);
    }
}