namespace ApplicationCore.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public int CatalogItemId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public string ProductName { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
}