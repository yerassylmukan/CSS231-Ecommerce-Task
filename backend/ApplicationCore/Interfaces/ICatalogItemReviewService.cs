using ApplicationCore.DTOs;

namespace ApplicationCore.Interfaces;

public interface ICatalogItemReviewService
{
    Task<IEnumerable<CatalogItemReviewDTO>> GetCatalogItemReviewsAsync(CancellationToken cancellationToken);
    Task<CatalogItemReviewDTO> GetCatalogItemReviewByIdAsync(int id, CancellationToken cancellationToken);
    Task<CatalogItemReviewDTO> GetCatalogItemReviewByUserIdAsync(string userId, CancellationToken cancellationToken);

    Task<CatalogItemReviewDTO> GetCatalogItemReviewByCatalogItemIdAsync(int catalogItemId,
        CancellationToken cancellationToken);

    Task<CatalogItemReviewDTO> CreateCatalogItemReviewAsync(string userId, decimal rating, string reviewText,
        int catalogItemId,
        CancellationToken cancellationToken);

    Task UpdateCatalogItemReviewAsync(int id, decimal rating, string reviewText, CancellationToken cancellationToken);
    Task DeleteCatalogItemReviewAsync(int id, CancellationToken cancellationToken);
}