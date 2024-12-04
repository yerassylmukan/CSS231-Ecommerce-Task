namespace WebApi.Models;

public class RequestPasswordResetModel
{
    public string Email { get; set; }
    public string LinkToResetPassword { get; set; }
}