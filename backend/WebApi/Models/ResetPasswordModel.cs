using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class ResetPasswordModel
{
    [Required] public string Email { get; set; }

    [Required] public string Token { get; set; }

    [Required] public string NewPassword { get; set; }
}