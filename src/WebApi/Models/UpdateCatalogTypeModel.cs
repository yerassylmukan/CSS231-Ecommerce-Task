using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class UpdateCatalogTypeModel
{
    [Required] public int CatalogTypeId { get; set; }
}