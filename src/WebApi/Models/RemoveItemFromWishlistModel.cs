using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class RemoveItemFromWishlistModel
{
    [Required] public int CatalogItemId { get; set; }
}