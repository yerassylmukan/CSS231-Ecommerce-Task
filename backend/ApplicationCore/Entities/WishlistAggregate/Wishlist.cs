namespace ApplicationCore.Entities.WishlistAggregate;

public class Wishlist : BaseEntity
{
    public Wishlist()
    {
    }

    public Wishlist(string userId)
    {
        if (string.IsNullOrEmpty(userId)) throw new ArgumentException("User ID cannot be null or empty.");
        UserId = userId;
    }

    public string UserId { get; private set; }
    private readonly List<WishlistItem> _items = new();
    public IReadOnlyCollection<WishlistItem> Items => _items.AsReadOnly();
}