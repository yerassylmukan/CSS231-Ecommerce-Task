using ApplicationCore.DTOs;

namespace ApplicationCore.Interfaces;

public interface IApplicationUserService
{
    Task<IEnumerable<ApplicationUserDTO>> GetUsersAsync(CancellationToken cancellationToken);
    Task<ApplicationUserDTO> GetUserDetailsByUserNameAsync(string userName);

    Task<ApplicationUserDTO> GetUserDetailsByEmailAsync(string email);

    Task<ApplicationUserDTO> GetUserDetailsByUserIdAsync(string userId);

    Task UpdateProfileInformationAsync(string userId, string firstName, string lastName,
        string profilePictureUrl);
}