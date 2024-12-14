namespace ApplicationCore.Entities;

public class CatalogType
{
    public int Id { get; set; }
    public string Type { get; set; }

    public ICollection<CatalogItem> CatalogItems { get; } = new List<CatalogItem>();
}