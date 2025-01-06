using ApplicationCore.DTOs;

namespace ApplicationCore.Interfaces;

public interface ICatalogBrandService
{
    Task<IEnumerable<CatalogBrandDTO>> GetCatalogBrandsAsync(CancellationToken cancellationToken);
    Task<CatalogBrandDTO> GetCatalogBrandByIdAsync(int id, CancellationToken cancellationToken);
    Task<CatalogBrandDTO> GetCatalogBrandByNameAsync(string catalogBrandName, CancellationToken cancellationToken);
    Task<CatalogBrandDTO> CreateCatalogBrandAsync(string catalogBrandName, CancellationToken cancellationToken);
    Task UpdateCatalogBrandAsync(int id, string catalogBrandName, CancellationToken cancellationToken);
    Task DeleteCatalogBrandAsync(int id, CancellationToken cancellationToken);
}