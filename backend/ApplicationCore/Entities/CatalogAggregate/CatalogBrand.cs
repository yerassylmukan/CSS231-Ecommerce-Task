namespace ApplicationCore.Entities.CatalogAggregate;

public class CatalogBrand : BaseEntity
{
    public CatalogBrand() { }
    
    public CatalogBrand(string brand)
    {
        Brand = brand;
    }

    public string Brand { get; private set; }
}