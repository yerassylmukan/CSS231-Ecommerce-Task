using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class CreateCatalogItemModel
{
    [Required] public string Name { get; set; }

    [Required] public string Description { get; set; }

    [Required] public decimal Price { get; set; }

    [Required] public string PictureUrl { get; set; }

    [Required] public int StockQuantity { get; set; }

    [Required] public int CatalogTypeId { get; set; }

    [Required] public int CatalogBrandId { get; set; }
}