namespace ApplicationCore.Entities;

public class WishlistItem
{
    public int Id { get; set; }
    public int CatalogItemId { get; set; }

    public int WishlistId { get; set; }
    public Wishlist Wishlist { get; set; } = null!;
}