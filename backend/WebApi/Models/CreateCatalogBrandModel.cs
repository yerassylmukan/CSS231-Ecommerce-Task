using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class CreateCatalogBrandModel
{
    [Required] public string BandName { get; set; }
}