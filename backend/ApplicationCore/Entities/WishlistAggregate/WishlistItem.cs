using ApplicationCore.Entities.CatalogAggregate;

namespace ApplicationCore.Entities.WishlistAggregate;

public class WishlistItem : BaseEntity
{
    public WishlistItem(int wishlistId, int catalogItemId, CatalogItem catalogItem)
    {
        WishlistId = wishlistId;
        CatalogItemId = catalogItemId;
        CatalogItem = catalogItem ?? throw new ArgumentNullException(nameof(catalogItem));
    }

    public int WishlistId { get; private set; }
    public Wishlist Wishlist { get; private set; }

    public int CatalogItemId { get; private set; }
    public CatalogItem CatalogItem { get; private set; }
}