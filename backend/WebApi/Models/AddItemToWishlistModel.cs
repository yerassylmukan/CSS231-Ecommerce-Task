using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class AddItemToWishlistModel
{
    [Required] public int CatalogItemId { get; set; }
}