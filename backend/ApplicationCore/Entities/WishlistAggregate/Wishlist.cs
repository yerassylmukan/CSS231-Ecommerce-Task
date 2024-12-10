namespace ApplicationCore.Entities.WishlistAggregate;

public class Wishlist : BaseEntity
{
    private readonly List<WishlistItem> _items = new();

    public Wishlist()
    {
    }

    public Wishlist(string userId)
    {
        if (string.IsNullOrEmpty(userId)) throw new ArgumentException("User ID cannot be null or empty.");
        UserId = userId;
    }

    public string UserId { get; private set; }
    public IReadOnlyCollection<WishlistItem> Items => _items.AsReadOnly();
}