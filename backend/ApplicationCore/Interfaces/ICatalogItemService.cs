using ApplicationCore.DTOs;

namespace ApplicationCore.Interfaces;

public interface ICatalogItemService
{
    Task<IEnumerable<CatalogItemDTO>> GetCatalogItemsAsync(CancellationToken cancellationToken);
    Task<CatalogItemDTO> GetCatalogItemByIdAsync(int id, CancellationToken cancellationToken);
    Task<CatalogItemDTO> GetCatalogItemsByTypeNameAsync(string catalogTypeName, CancellationToken cancellationToken);
    Task<CatalogItemDTO> GetCatalogItemsByBrandNameAsync(string catalogBrandName, CancellationToken cancellationToken);

    Task<CatalogItemDTO> CreateCatalogItemAsync(string name, string description, decimal price,
        string pictureUrl, int stockQuantity, int catalogTypeId, int catalogBrandId,
        CancellationToken cancellationToken);

    Task UpdateCatalogItemDetailsAsync(int id, string name, string description, decimal price,
        CancellationToken cancellationToken);

    Task UpdateCatalogItemStockQuantityAsync(int id, int stockQuantity, CancellationToken cancellationToken);
    Task UpdateCatalogItemPictureUrlAsync(int id, string pictureUrl, CancellationToken cancellationToken);
    Task UpdateCatalogTypeAsync(int id, int catalogTypeId, CancellationToken cancellationToken);
    Task UpdateCatalogBrandAsync(int id, int catalogBrandId, CancellationToken cancellationToken);
    Task DeleteCatalogItemAsync(int id, CancellationToken cancellationToken);
}