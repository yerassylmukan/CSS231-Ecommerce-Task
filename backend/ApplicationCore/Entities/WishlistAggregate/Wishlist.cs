using ApplicationCore.Entities.CatalogAggregate;

namespace ApplicationCore.Entities.WishlistAggregate;

public class Wishlist : BaseEntity
{
    public Wishlist(string userId)
    {
        if (string.IsNullOrEmpty(userId)) throw new ArgumentException("User ID cannot be null or empty.");
        UserId = userId;
    }

    public string UserId { get; private set; }
    public List<WishlistItem> Items { get; } = new();

    public void AddItem(int catalogItemId, CatalogItem catalogItem)
    {
        if (Items.Any(item => item.CatalogItemId == catalogItemId))
            throw new InvalidOperationException("Item already exists in the wishlist.");

        Items.Add(new WishlistItem(Id, catalogItemId, catalogItem));
    }

    public void RemoveItem(int catalogItemId)
    {
        var item = Items.FirstOrDefault(i => i.CatalogItemId == catalogItemId);
        if (item is null) throw new InvalidOperationException("Item not found in the wishlist.");
        Items.Remove(item);
    }
}