using ApplicationCore.DTOs;

namespace ApplicationCore.Interfaces;

public interface IWishlistService
{
    Task<WishlistDTO> GetWishlistAsync(string userId, CancellationToken cancellationToken);
    Task<WishlistDTO> AddItemToWishlistAsync(string userId, int catalogItemId, CancellationToken cancellationToken);
    Task RemoveItemFromWishlistAsync(string userId, int catalogItemId, CancellationToken cancellationToken);
    Task RemoveWishlistAsync(int wishlistId, CancellationToken cancellationToken);
    Task RemoveWishlistByUserIdAsync(string userId, CancellationToken cancellationToken);
}