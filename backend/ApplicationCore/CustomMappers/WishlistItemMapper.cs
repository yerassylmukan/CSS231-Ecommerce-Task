using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.CustomMappers;

public static class WishlistItemMapper
{
    public static WishlistItemDTO MapToDTO(this WishlistItem wishlistItem)
    {
        return new WishlistItemDTO
        {
            Id = wishlistItem.Id,
            CatalogItemId = wishlistItem.CatalogItemId,
            ProductName = wishlistItem.ProductName,
            PictureUrl = wishlistItem.PictureUrl,
            WishlistId = wishlistItem.WishlistId
        };
    }
}