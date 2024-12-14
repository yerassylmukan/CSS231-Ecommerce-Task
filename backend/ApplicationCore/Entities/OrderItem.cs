namespace ApplicationCore.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public int CatalogItemId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Units { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
}