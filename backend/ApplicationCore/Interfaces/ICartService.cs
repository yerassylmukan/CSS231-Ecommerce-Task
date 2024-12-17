using ApplicationCore.DTOs;

namespace ApplicationCore.Interfaces;

public interface ICartService
{
    Task<CartDTO> GetCartAsync(string userId, CancellationToken cancellationToken);

    Task<CartDTO> AddItemToCartAsync(string userId, int catalogItemId, CancellationToken cancellationToken,
        int quantity = 1);

    Task<CartDTO> UpdateItemQuantityAsync(string userId, int catalogItemId, int quantity,
        CancellationToken cancellationToken);

    Task RemoveItemFromCartAsync(string userId, int catalogItemId, CancellationToken cancellationToken);
    Task RemoveCartAsync(int cartId, CancellationToken cancellationToken);
    Task RemoveCartByUserIdAsync(string userId, CancellationToken cancellationToken);
}