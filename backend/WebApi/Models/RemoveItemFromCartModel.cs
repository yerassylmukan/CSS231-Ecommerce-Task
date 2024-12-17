using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class RemoveItemFromCartModel
{
    [Required] public int CatalogItemId { get; set; }
}