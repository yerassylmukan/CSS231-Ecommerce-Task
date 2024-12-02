using ApplicationCore.Common.Models;

namespace ApplicationCore.Common.Contracts;

public interface IIdentityService
{
    Task<bool> UserExists(string email);

    Task<Result> AddToRolesAsync(string userId, List<string> roles);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthenticateAsync(string username, string password);

    Task<(Result Result, string UserId)> CreateUserAsync(string firstName, string lastName, string userName,
        string profilePictureUrl, string email, string password);

    Task<(Result Result, string UserId)> CreateExternalUserAsync(string firstName, string lastName, string userName,
        string profilePictureUrl, string email, string loginProvider, string providerKey, string providerName);

    Task<Result> DeleteUserAsync(string userId);

    Task<Result> UpdatePasswordAsync(string userId, string oldPassword, string newPassword);

    Task<Result> UpdateEmailAsync(string userId, string email);
}