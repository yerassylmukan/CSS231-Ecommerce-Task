namespace ApplicationCore.Entities.CatalogAggregate;

public class CatalogItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string PictureUrl { get; set; }
    public int StockQuantity { get; set; }

    public int CatalogTypeId { get; set; }
    public CatalogType CatalogType { get; set; } = null!;

    public int CatalogBrandId { get; set; }
    public CatalogBrand CatalogBrand { get; set; } = null!;

    public ICollection<CatalogItemReview> Reviews { get; } = new List<CatalogItemReview>();
}