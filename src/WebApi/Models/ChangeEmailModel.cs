using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class ChangeEmailModel
{
    [Required] public string OldEmail { get; set; }
    [Required] public string NewEmail { get; set; }
}