using ApplicationCore.Entities.CatalogAggregate;

namespace ApplicationCore.Entities.BasketAggregate;

public class Cart : BaseEntity
{
    public Cart(string userId)
    {
        if (string.IsNullOrEmpty(userId)) throw new ArgumentException("User ID cannot be null or empty.");
        UserId = userId;
    }

    public string UserId { get; private set; }
    public List<CartItem> Items { get; } = new();

    public void AddItem(int catalogItemId, int quantity, CatalogItem catalogItem)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.");
        var existingItem = Items.FirstOrDefault(item => item.CatalogItemId == catalogItemId);

        if (existingItem is not null)
            existingItem.UpdateQuantity(existingItem.Quantity + quantity);
        else
            Items.Add(new CartItem(Id, catalogItemId, catalogItem, quantity));
    }

    public void RemoveItem(int catalogItemId)
    {
        var item = Items.FirstOrDefault(i => i.CatalogItemId == catalogItemId);
        if (item is null) throw new InvalidOperationException("Item not found in cart.");
        Items.Remove(item);
    }

    public void UpdateItemQuantity(int catalogItemId, int quantity)
    {
        if (quantity < 0) throw new ArgumentException("Quantity must be non-negative.");
        var item = Items.FirstOrDefault(i => i.CatalogItemId == catalogItemId);
        if (item is null) throw new InvalidOperationException("Item not found in cart.");
        item.UpdateQuantity(quantity);
    }
}