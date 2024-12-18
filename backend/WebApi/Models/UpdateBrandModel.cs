using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class UpdateBrandModel
{
    [Required] public string Brand { get; set; }
}