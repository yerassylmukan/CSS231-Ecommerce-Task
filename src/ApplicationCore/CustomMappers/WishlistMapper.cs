using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.CustomMappers;

public static class WishlistMapper
{
    public static WishlistDTO MapToDTO(this Wishlist wishlist)
    {
        return new WishlistDTO
        {
            Id = wishlist.Id,
            UserId = wishlist.UserId,
            Items = wishlist.Items.Select(wi => wi.MapToDTO()).ToList()
        };
    }
}