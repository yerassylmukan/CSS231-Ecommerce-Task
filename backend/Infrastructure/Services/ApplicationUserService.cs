using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ApplicationUserService : IApplicationUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationUserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async
        Task<(string UserId, string FirstName, string LastName, string UserName, string Email, string profilePictureUrl,
            IEnumerable<string> Roles)>
        GetUserDetailsByUserNameAsync(string userName)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        if (user == null) throw new UserNotFoundException(userName);

        var roles = await _userManager.GetRolesAsync(user);

        return (user.Id!, user.FirstName!, user.LastName!, user.UserName!, user.Email!, user.ProfilePictureUrl, roles);
    }

    public async
        Task<(string UserId, string FirstName, string LastName, string UserName, string Email, string profilePictureUrl,
            IEnumerable<string> Roles)>
        GetUserDetailsByEmailAsync(string email)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user == null) throw new UserNotFoundException(email);

        var roles = await _userManager.GetRolesAsync(user);

        return (user.Id!, user.FirstName!, user.LastName!, user.UserName!, user.Email!, user.ProfilePictureUrl, roles);
    }

    public async
        Task<(string UserId, string FirstName, string LastName, string UserName, string Email, string profilePictureUrl,
            IEnumerable<string> Roles)> GetUserDetailsByUserIdAsync(string userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null) throw new UserNotFoundException(userId);
        var roles = await _userManager.GetRolesAsync(user);

        return (user.Id!, user.FirstName!, user.LastName!, user.UserName!, user.Email!, user.ProfilePictureUrl, roles);
    }

    public async Task UpdateProfileInformationAsync(string userId, string firstName, string lastName,
        string profilePictureUrl)
    {
        if (userId == null) throw new ArgumentNullException(nameof(userId));
        
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null) throw new UserNotFoundException(userId);

        var isUpdated = false;

        if (!string.IsNullOrEmpty(firstName) && user.FirstName != firstName)
        {
            user.FirstName = firstName;
            isUpdated = true;
        }

        if (!string.IsNullOrEmpty(lastName) && user.LastName != lastName)
        {
            user.LastName = lastName;
            isUpdated = true;
        }

        if (!string.IsNullOrEmpty(profilePictureUrl))
        {
            user.ProfilePictureUrl = profilePictureUrl;
            isUpdated = true;
        }

        if (isUpdated) await _userManager.UpdateAsync(user);
    }
}