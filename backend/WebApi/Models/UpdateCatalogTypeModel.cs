using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class UpdateCatalogTypeModel
{
    [Required] public string Type { get; set; }
}