using ApplicationCore.Entities.CatalogAggregate;

namespace ApplicationCore.Entities.BasketAggregate;

public class Cart : BaseEntity
{
    public Cart() { }
    
    public Cart(string userId)
    {
        if (string.IsNullOrEmpty(userId)) throw new ArgumentException("User ID cannot be null or empty.");
        UserId = userId;
    }

    public string UserId { get; private set; }
    
    private readonly List<CartItem> _items = new List<CartItem>();
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();
    public int TotalItems => Items.Sum(item => item.Quantity);

    public void AddItem(int catalogItemId, int unitPrice, int quantity = 1)
    {
        if (quantity <= 0) 
            throw new ArgumentException("Quantity must be greater than zero.");

        if (!Items.Any(i => i.CatalogItemId == catalogItemId))
        {
            _items.Add(new CartItem(catalogItemId, quantity, unitPrice));
        }
        var existingItem = Items.FirstOrDefault(i => i.CatalogItemId == catalogItemId);
        existingItem.AddQuantity(quantity);
    }

    public void RemoveItem(int catalogItemId)
    {
        _items.RemoveAll(i => i.Quantity == 0);
    }

    public void UpdateUserId(string userId)
    {
        UserId = userId;
    }
}