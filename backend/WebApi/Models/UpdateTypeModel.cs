using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class UpdateTypeModel
{
    [Required] public string Type { get; set; }
}