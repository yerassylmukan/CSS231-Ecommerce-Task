namespace ApplicationCore.DTOs;

public class ApplicationUserDTO
{
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string ProfilePictureUrl { get; set; }
    public IEnumerable<string> Roles { get; set; }
}