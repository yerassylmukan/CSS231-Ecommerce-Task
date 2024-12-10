namespace ApplicationCore.Entities.CatalogAggregate;

public class CatalogType : BaseEntity
{
    public CatalogType()
    {
    }

    public CatalogType(string type)
    {
        Type = type;
    }

    public string Type { get; private set; }
}