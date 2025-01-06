using ApplicationCore.DTOs;

namespace ApplicationCore.Interfaces;

public interface IReviewService
{
    Task<IEnumerable<CatalogItemReviewDTO>> GetReviewsAsync(int catalogItemId, CancellationToken cancellationToken);
    Task<IEnumerable<CatalogItemReviewDTO>> GetReviewsByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<CatalogItemReviewDTO> GetReviewByIdAsync(int id, CancellationToken cancellationToken);

    Task<CatalogItemReviewDTO> CreateReviewAsync(string userId, decimal rating, string reviewText,
        int catalogItemId,
        CancellationToken cancellationToken);

    Task UpdateReviewAsync(int id, string userId, decimal rating, string reviewText,
        CancellationToken cancellationToken);

    Task DeleteReviewAsync(int id, string userId, CancellationToken cancellationToken);
}