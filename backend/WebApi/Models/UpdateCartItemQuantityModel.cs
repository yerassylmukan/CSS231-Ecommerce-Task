using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class UpdateCartItemQuantityModel
{
    [Required] public int CatalogItemId { get; set; }

    [Required] public int Quantity { get; set; }
}