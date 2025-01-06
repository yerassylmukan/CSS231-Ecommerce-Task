using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class UpdateCatalogBrandModel
{
    [Required] public int CatalogBrandId { get; set; }
}