using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.CustomMappers;

public static class CartItemMapper
{
    public static CartItemDTO MapToDTO(this CartItem cartItem)
    {
        return new CartItemDTO
        {
            Id = cartItem.Id,
            CatalogItemId = cartItem.CatalogItemId,
            UnitPrice = cartItem.UnitPrice,
            Quantity = cartItem.Quantity,
            ProductName = cartItem.ProductName,
            PictureUrl = cartItem.PictureUrl,
            CartId = cartItem.CartId
        };
    }
}