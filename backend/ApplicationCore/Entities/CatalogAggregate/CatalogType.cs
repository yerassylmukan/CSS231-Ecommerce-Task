namespace ApplicationCore.Entities.CatalogAggregate;

public class CatalogType : BaseEntity
{
    public CatalogType(string type)
    {
        Type = type;
    }

    public string Type { get; private set; }

    public ICollection<CatalogItem> CatalogItems { get; private set; } = new List<CatalogItem>();
}