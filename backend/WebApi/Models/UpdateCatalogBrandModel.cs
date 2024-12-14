using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class UpdateCatalogBrandModel
{
    [Required] public string BandName { get; set; }
}