namespace ApplicationCore.Interfaces;

public interface IIdentityService
{
    Task<string> CreateUserAsync(string email, string password, string firstName, string lastName, string profilePictureUrl);
    Task<string> AuthenticateUserAsync(string email, string password);
    Task Logout();
}