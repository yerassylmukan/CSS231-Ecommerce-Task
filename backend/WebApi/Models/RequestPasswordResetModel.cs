using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class RequestPasswordResetModel
{
    [Required] public string Email { get; set; }

    [Required] public string LinkToResetPassword { get; set; }
}