using ApplicationCore.Entities.CatalogAggregate;

namespace ApplicationCore.Entities.OrderAggregate;

public class OrderItem
{
    public OrderItem(int orderId, int catalogItemId, CatalogItem catalogItem, int quantity, decimal unitPrice)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.");
        if (unitPrice <= 0) throw new ArgumentException("Unit price must be greater than zero.");
        if (catalogItem == null) throw new ArgumentNullException(nameof(catalogItem));

        OrderId = orderId;
        CatalogItemId = catalogItemId;
        CatalogItem = catalogItem;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public int OrderId { get; private set; }
    public Order Order { get; private set; }

    public int CatalogItemId { get; private set; }
    public CatalogItem CatalogItem { get; private set; }

    public int Quantity { get; }
    public decimal UnitPrice { get; }

    public decimal TotalPrice => Quantity * UnitPrice;
}