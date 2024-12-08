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

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async
        Task<(string UserId, string FirstName, string LastName, string UserName, string Email, string profilePictureUrl,
            IEnumerable<string> Roles)>
        GetUserDetailsByUserNameAsync(string userName)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        if (user == null) throw new UserNotFoundException("User not found");

        var roles = await _userManager.GetRolesAsync(user);

        return (user.Id!, user.FirstName!, user.LastName!, user.UserName!, user.Email!, user.ProfilePictureUrl, roles);
    }

    public async
        Task<(string UserId, string FirstName, string LastName, string UserName, string Email, string profilePictureUrl,
            IEnumerable<string> Roles)>
        GetUserDetailsByEmailAsync(string email)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user == null) throw new UserNotFoundException("User not found");

        var roles = await _userManager.GetRolesAsync(user);

        return (user.Id!, user.FirstName!, user.LastName!, user.UserName!, user.Email!, user.ProfilePictureUrl, roles);
    }

    public async
        Task<(string UserId, string FirstName, string LastName, string UserName, string Email, string profilePictureUrl,
            IEnumerable<string> Roles)> GetUserDetailsByUserIdAsync(string userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null) throw new UserAlreadyExistsException("User not found");
        var roles = await _userManager.GetRolesAsync(user);

        return (user.Id!, user.FirstName!, user.LastName!, user.UserName!, user.Email!, user.ProfilePictureUrl, roles);
    }

    public async Task UpdateProfileInformationAsync(string userId, string firstName, string lastName, string email,
        string profilePictureUrl)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null) throw new UserNotFoundException("User not found");

        var isUpdated = false;

        if (user.FirstName != firstName)
        {
            user.FirstName = firstName;
            isUpdated = true;
        }

        if (user.LastName != lastName)
        {
            user.LastName = lastName;
            isUpdated = true;
        }

        if (!string.IsNullOrEmpty(email) && user.Email != email)
        {
            user.Email = email;
            user.UserName = email;
            isUpdated = true;
        }

        if (isUpdated) await _userManager.UpdateAsync(user);
    }
}