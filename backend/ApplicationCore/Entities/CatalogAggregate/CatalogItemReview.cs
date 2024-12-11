namespace ApplicationCore.Entities.CatalogAggregate;

public class CatalogItemReview
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public decimal Rating { get; set; }
    public string ReviewText { get; set; }
    public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;

    public int? CatalogItemId { get; set; }
    public CatalogItem? CatalogItem { get; set; }
}