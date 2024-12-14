namespace ApplicationCore.Interfaces;

public interface IIdentityService
{
    Task<string> CreateUserAsync(string email, string password, string firstName, string lastName,
        string profilePictureUrl);

    Task<string> AuthenticateUserAsync(string email, string password);
    string AuthenticateAnonymousUser();
    Task<string> AddUserToRolesAsync(string email, IEnumerable<string> roles);
    Task SendPasswordResetTokenAsync(string email, string linkToResetPassword);
    Task ResetPasswordAsync(string email, string token, string newPassword);
}