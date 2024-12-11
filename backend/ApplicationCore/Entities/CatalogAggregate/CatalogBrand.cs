namespace ApplicationCore.Entities.CatalogAggregate;

public class CatalogBrand
{
    public int Id { get; set; }
    public string Brand { get; set; }

    public ICollection<CatalogItem> CatalogItems { get; } = new List<CatalogItem>();
}