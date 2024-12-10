namespace ApplicationCore.Entities.OrderAggregate;

public class OrderItem : BaseEntity
{
    public OrderItem()
    {
    }

    public OrderItem(OrderedCatalogItem orderedCatalogItem, decimal unitPrice, int units)
    {
        if (orderedCatalogItem == null) throw new ArgumentNullException(nameof(orderedCatalogItem));
        if (unitPrice <= 0) throw new ArgumentException("Unit price must be greater than zero.");
        if (units <= 0) throw new ArgumentException("Quantity must be greater than zero.");

        OrderedCatalogItem = orderedCatalogItem;
        UnitPrice = unitPrice;
        Units = units;
    }

    public OrderedCatalogItem OrderedCatalogItem { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int Units { get; private set; }
}