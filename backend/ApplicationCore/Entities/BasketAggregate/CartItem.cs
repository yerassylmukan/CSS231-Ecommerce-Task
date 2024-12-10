using ApplicationCore.Entities.CatalogAggregate;

namespace ApplicationCore.Entities.BasketAggregate;

public class CartItem : BaseEntity
{
    public CartItem(int cartId, int catalogItemId, CatalogItem catalogItem, int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.");
        CartId = cartId;
        CatalogItemId = catalogItemId;
        CatalogItem = catalogItem ?? throw new ArgumentNullException(nameof(catalogItem));
        Quantity = quantity;
    }

    public int CartId { get; private set; }
    public Cart Cart { get; private set; }

    public int CatalogItemId { get; private set; }
    public CatalogItem CatalogItem { get; private set; }

    public int Quantity { get; private set; }

    public void UpdateQuantity(int quantity)
    {
        if (quantity < 0) throw new ArgumentException("Quantity must be non-negative.");
        Quantity = quantity;
    }
}