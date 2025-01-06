namespace WebApi.Models;

public class UpdateCatalogItemModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
}