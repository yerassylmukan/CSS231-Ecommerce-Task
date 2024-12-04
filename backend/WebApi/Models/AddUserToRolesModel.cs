namespace WebApi.Models;

public class AddUserToRolesModel
{
    public string Email { get; set; }
    public IEnumerable<string> Roles { get; set; }
}