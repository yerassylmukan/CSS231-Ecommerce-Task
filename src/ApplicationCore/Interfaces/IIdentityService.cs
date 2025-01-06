namespace ApplicationCore.Interfaces;

public interface IIdentityService
{
    Task<string> CreateUserAsync(string email, string password, string firstName, string lastName,
        string profilePictureUrl);

    Task<string> AuthenticateUserAsync(string email, string password);
    string AuthenticateAnonymousUser();
    Task<string> AddUserToRolesAsync(string email, IEnumerable<string> roles);
    Task SendPasswordResetTokenAsync(string email, string linkToResetPassword);
    Task ResetPasswordAsync(string email, int code, string newPassword);
    Task<string> ChangeEmailAsync(string email, string newEmail);
    Task ChangePasswordAsync(string email, string oldPassword, string newPassword);
}