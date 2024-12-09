namespace ApplicationCore.Entities.CatalogAggregate;

public class CatalogBrand : BaseEntity
{
    public CatalogBrand(string brand)
    {
        Brand = brand;
    }

    public string Brand { get; private set; }

    public ICollection<CatalogItem> CatalogItems { get; private set; } = new List<CatalogItem>();
}