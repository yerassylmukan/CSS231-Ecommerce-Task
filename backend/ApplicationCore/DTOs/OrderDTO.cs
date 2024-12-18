using ApplicationCore.Entities;

namespace ApplicationCore.DTOs;

public class OrderDTO
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public bool IsConfirmed { get; set; }
    public ShippingMethod ShippingMethod { get; set; }
    public List<OrderItemDTO> Items { get; set; }
    public decimal TotalPrice => Items.Sum(item => item.TotalPrice) + ShippingMethod.Cost;
}