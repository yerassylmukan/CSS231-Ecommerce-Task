using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class UpdateCatalogItemStockQuantityModel
{
    [Required] public int StockQuantity { get; set; }
}