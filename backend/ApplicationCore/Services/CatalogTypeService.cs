using ApplicationCore.CustomMappers;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services;

public class CatalogTypeService : ICatalogTypeService
{
    private readonly IApplicationDbContext _context;

    public CatalogTypeService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CatalogTypeDTO>> GetCatalogTypesAsync(CancellationToken cancellationToken)
    {
        var catalogTypes = await _context.CatalogTypes.ToListAsync(cancellationToken);

        var result = catalogTypes.Select(ct => ct.MapToDTO());

        return result;
    }

    public async Task<CatalogTypeDTO> GetCatalogTypeByIdAsync(int id, CancellationToken cancellationToken)
    {
        var catalogType = await _context.CatalogTypes.FirstOrDefaultAsync(cb => cb.Id == id, cancellationToken);

        if (catalogType == null)
            throw new CatalogTypeDoesNotExistsException(id);

        return catalogType.MapToDTO();
    }

    public async Task<CatalogTypeDTO> GetCatalogTypeByNameAsync(string catalogTypeName,
        CancellationToken cancellationToken)
    {
        var catalogType =
            await _context.CatalogTypes.FirstOrDefaultAsync(cb => cb.Type == catalogTypeName, cancellationToken);

        if (catalogType == null)
            throw new CatalogTypeDoesNotExistsException(catalogTypeName);

        return catalogType.MapToDTO();
    }

    public async Task<CatalogTypeDTO> CreateCatalogTypeAsync(string catalogTypeName,
        CancellationToken cancellationToken)
    {
        var typeExists =
            await _context.CatalogTypes.FirstOrDefaultAsync(cb => cb.Type == catalogTypeName, cancellationToken);

        if (typeExists != null) throw new CatalogTypeAlreadyExistsException(catalogTypeName);

        var catalogType = new CatalogType
        {
            Type = catalogTypeName
        };

        var result = _context.CatalogTypes.Add(catalogType);
        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity.MapToDTO();
    }

    public async Task UpdateCatalogTypeAsync(int id, string catalogTypeName, CancellationToken cancellationToken)
    {
        var catalogType = await _context.CatalogTypes.FirstOrDefaultAsync(cb => cb.Id == id, cancellationToken);

        if (catalogType == null)
            throw new CatalogTypeDoesNotExistsException(id);

        if (string.IsNullOrEmpty(catalogTypeName))
            throw new ArgumentException($"'{nameof(catalogTypeName)}' cannot be null or empty.");

        if (catalogTypeName != catalogType.Type)
        {
            catalogType.Type = catalogTypeName;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteCatalogTypeAsync(int id, CancellationToken cancellationToken)
    {
        var catalogType = await _context.CatalogTypes.FirstOrDefaultAsync(cb => cb.Id == id, cancellationToken);

        if (catalogType == null)
            throw new CatalogTypeDoesNotExistsException(id);

        _context.CatalogTypes.Remove(catalogType);
        await _context.SaveChangesAsync(cancellationToken);
    }
}