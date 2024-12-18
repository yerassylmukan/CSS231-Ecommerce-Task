using ApplicationCore.DTOs;
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

    public async Task<IEnumerable<ApplicationUserDTO>> GetUsersAsync(CancellationToken cancellationToken)
    {
        var users = await _userManager.Users.ToListAsync();

        var usersDto = new List<ApplicationUserDTO>();

        foreach (var user in users)
        {
            var userDto = new ApplicationUserDTO
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                ProfilePictureUrl = user.ProfilePictureUrl,
                Roles = await _userManager.GetRolesAsync(user)
            };

            usersDto.Add(userDto);
        }

        return usersDto;
    }

    public async Task<ApplicationUserDTO> GetUserDetailsByUserNameAsync(string userName)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        if (user == null) throw new UserNotFoundException(userName);

        var roles = await _userManager.GetRolesAsync(user);

        var userDto = new ApplicationUserDTO
        {
            UserId = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email,
            ProfilePictureUrl = user.ProfilePictureUrl,
            Roles = roles
        };

        return userDto;
    }

    public async
        Task<ApplicationUserDTO>
        GetUserDetailsByEmailAsync(string email)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user == null) throw new UserNotFoundException(email);

        var roles = await _userManager.GetRolesAsync(user);

        var userDto = new ApplicationUserDTO
        {
            UserId = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email,
            ProfilePictureUrl = user.ProfilePictureUrl,
            Roles = roles
        };

        return userDto;
    }

    public async
        Task<ApplicationUserDTO> GetUserDetailsByUserIdAsync(string userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null) throw new UserNotFoundException(userId);

        var roles = await _userManager.GetRolesAsync(user);

        var userDto = new ApplicationUserDTO
        {
            UserId = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email,
            ProfilePictureUrl = user.ProfilePictureUrl,
            Roles = roles
        };

        return userDto;
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