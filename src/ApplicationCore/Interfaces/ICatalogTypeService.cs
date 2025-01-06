using ApplicationCore.DTOs;

namespace ApplicationCore.Interfaces;

public interface ICatalogTypeService
{
    Task<IEnumerable<CatalogTypeDTO>> GetCatalogTypesAsync(CancellationToken cancellationToken);
    Task<CatalogTypeDTO> GetCatalogTypeByIdAsync(int id, CancellationToken cancellationToken);
    Task<CatalogTypeDTO> GetCatalogTypeByNameAsync(string catalogTypeName, CancellationToken cancellationToken);
    Task<CatalogTypeDTO> CreateCatalogTypeAsync(string catalogTypeName, CancellationToken cancellationToken);
    Task UpdateCatalogTypeAsync(int id, string catalogTypeName, CancellationToken cancellationToken);
    Task DeleteCatalogTypeAsync(int id, CancellationToken cancellationToken);
}