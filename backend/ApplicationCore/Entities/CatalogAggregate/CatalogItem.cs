namespace ApplicationCore.Entities.CatalogAggregate;

public class CatalogItem : BaseEntity
{
    public CatalogItem(int catalogTypeId, int catalogBrandId, string name, string description, decimal price,
        string pictureUrl, int initialStock)
    {
        if (initialStock <= 0) throw new ArgumentException("Initial stock must be greater than zero.");
        CatalogTypeId = catalogTypeId;
        CatalogBrandId = catalogBrandId;
        Name = name;
        Description = description;
        Price = price;
        PictureUrl = pictureUrl;
        StockQuantity = initialStock;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public string PictureUrl { get; private set; }

    public int CatalogTypeId { get; private set; }
    public CatalogType? CatalogType { get; private set; }

    public int CatalogBrandId { get; private set; }
    public CatalogBrand? CatalogBrand { get; private set; }

    public int StockQuantity { get; private set; }

    public void UpdateDetails(CatalogItemDetails catalogItemDetails)
    {
        Name = catalogItemDetails.Name;
        Description = catalogItemDetails.Description;
        Price = catalogItemDetails.Price;
    }

    public void UpdateCatalogBrand(int catalogBrandId)
    {
        CatalogBrandId = catalogBrandId;
    }

    public void UpdateCatalogType(int catalogTypeId)
    {
        CatalogTypeId = catalogTypeId;
    }

    public void UpdatePictureUrl(string pictureUrl)
    {
        if (string.IsNullOrEmpty(pictureUrl))
        {
            PictureUrl = string.Empty;
            return;
        }

        PictureUrl = pictureUrl;
    }

    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity to increase must be greater than zero.");
        StockQuantity += quantity;
    }

    public void DecreaseStock(int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity to decrease must be greater than zero.");
        if (StockQuantity < quantity)
            throw new InvalidOperationException("Insufficient stock to complete the operation.");

        StockQuantity -= quantity;
    }

    public bool IsInStock(int requestedQuantity)
    {
        return StockQuantity >= requestedQuantity;
    }

    public readonly record struct CatalogItemDetails
    {
        public CatalogItemDetails(string? name, string? description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public string? Name { get; }
        public string? Description { get; }
        public decimal Price { get; }
    }
}