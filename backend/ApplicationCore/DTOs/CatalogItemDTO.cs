using ApplicationCore.Entities.CatalogAggregate;

namespace ApplicationCore.DTOs;

public class CatalogItemDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string PictureUrl { get; set; }
    public int StockQuantity { get; set; }
    public int CatalogTypeId { get; set; }
    public int CatalogBrandId { get; set; }
    public List<CatalogItemReviewDTO> Reviews { get; set; }
}