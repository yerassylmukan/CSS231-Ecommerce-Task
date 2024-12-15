namespace Infrastructure.Identity;

public class PasswordResetData
{
    public int Code { get; set; }
    public string Email { get; set; }
}