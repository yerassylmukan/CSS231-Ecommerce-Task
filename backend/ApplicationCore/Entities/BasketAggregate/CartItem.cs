namespace ApplicationCore.Entities.BasketAggregate;

public class CartItem : BaseEntity
{
    public CartItem()
    {
    }

    public CartItem(int catalogItemId, int quantity, decimal unitPrice)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        CatalogItemId = catalogItemId;
        UnitPrice = unitPrice;
        SetQuantity(quantity);
    }

    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public int CatalogItemId { get; private set; }
    public int CartId { get; private set; }

    public void AddQuantity(int quantity)
    {
        if (quantity < 0) throw new ArgumentException("Quantity must be non-negative.");
        Quantity += quantity;
    }

    public void SetQuantity(int quantity)
    {
        if (quantity < 0) throw new ArgumentException("Quantity must be non-negative.");
        Quantity = quantity;
    }
}