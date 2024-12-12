using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class RegisterModel
{
    [Required] public string Email { get; set; }

    [Required] public string Password { get; set; }

    [Required] public string FirstName { get; set; }

    [Required] public string LastName { get; set; }

    public string ProfilePictureUrl { get; set; }
}