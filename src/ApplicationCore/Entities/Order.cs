namespace ApplicationCore.Entities;

public class Order
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public ShippingMethod ShippingMethod { get; set; }
    public ShippingDetails ShippingDetails { get; set; }
    public bool IsConfirmed { get; set; } = false;

    public ICollection<OrderItem> Items { get; } = new List<OrderItem>();
}