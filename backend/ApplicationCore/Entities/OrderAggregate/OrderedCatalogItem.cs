namespace ApplicationCore.Entities.OrderAggregate;

public class OrderedCatalogItem
{
    private OrderedCatalogItem() { }

    public OrderedCatalogItem(int catalogItemId, string productName, string pictureUri)
    {
        CatalogItemId = catalogItemId;
        ProductName = productName;
        PictureUri = pictureUri;
    }

    public int CatalogItemId { get; private set; }
    public string ProductName { get; private set; }
    public string PictureUri { get; private set; }
}