namespace ApplicationCore.Entities;

public class Wishlist
{
    public int Id { get; set; }
    public string UserId { get; set; }

    public ICollection<WishlistItem> Items { get; } = new List<WishlistItem>();
}