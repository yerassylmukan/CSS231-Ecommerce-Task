using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.CustomMappers;

public static class CartMapper
{
    public static CartDTO MapToDTO(this Cart cart)
    {
        return new CartDTO
        {
            Id = cart.Id,
            UserId = cart.UserId,
            Items = cart.Items.Select(ci => ci.MapToDTO()).ToList()
        };
    }
}