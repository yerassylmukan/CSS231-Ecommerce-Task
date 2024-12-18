using ApplicationCore.DTOs;

namespace ApplicationCore.Interfaces;

public interface IApplicationUserService
{
    Task<IEnumerable<ApplicationUserDTO>> GetUsersAsync(CancellationToken cancellationToken);
    Task<(string UserId, string FirstName, string LastName, string UserName, string Email, string profilePictureUrl,
            IEnumerable<string> Roles)>
        GetUserDetailsByUserNameAsync(string userName);

    Task<(string UserId, string FirstName, string LastName, string UserName, string Email, string profilePictureUrl,
            IEnumerable<string> Roles)>
        GetUserDetailsByEmailAsync(string email);

    Task<(string UserId, string FirstName, string LastName, string UserName, string Email, string profilePictureUrl,
            IEnumerable<string> Roles)>
        GetUserDetailsByUserIdAsync(string userId);

    Task UpdateProfileInformationAsync(string userId, string firstName, string lastName,
        string profilePictureUrl);
}