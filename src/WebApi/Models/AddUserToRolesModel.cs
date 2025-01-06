using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class AddUserToRolesModel
{
    [Required] public string Email { get; set; }

    [Required] public IEnumerable<string> Roles { get; set; }
}