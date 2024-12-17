using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.CustomMappers;

public static class OrderItemMapper
{
    public static OrderItemDTO MapToDTO(this OrderItem orderItem)
    {
        return new OrderItemDTO
        {
            Id = orderItem.Id,
            CatalogItemId = orderItem.CatalogItemId,
            UnitPrice = orderItem.UnitPrice,
            Quantity = orderItem.Quantity,
            ProductName = orderItem.ProductName,
            OrderId = orderItem.OrderId
        };
    }
}