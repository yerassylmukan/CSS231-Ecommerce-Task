namespace ApplicationCore.Entities.WishlistAggregate;

public class WishlistItem : BaseEntity
{
    public WishlistItem()
    {
    }

    public WishlistItem(int catalogItemId)
    {
        CatalogItemId = catalogItemId;
    }

    public int WishlistId { get; private set; }
    public int CatalogItemId { get; private set; }
}