using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class CreateCatalogTypeModel
{
    [Required] public string TypeName { get; set; }
}