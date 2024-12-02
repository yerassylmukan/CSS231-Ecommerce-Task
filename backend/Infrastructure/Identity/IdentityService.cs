using ApplicationCore.Common.Contracts;
using ApplicationCore.Common.Exceptions;
using ApplicationCore.Common.Models;
using ApplicationCore.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    public async Task<(Result Result, string UserId)> CreateUserAsync(string firstName, string lastName,
        string userName, string profilePictureUrl, string email, string password)
    {
        var user = new ApplicationUser
        {
            FirstName = firstName,
            LastName = lastName,
            UserName = userName,
            ProfilePictureUrl = profilePictureUrl,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, password);

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<(Result Result, string UserId)> CreateExternalUserAsync(string firstName, string lastName,
        string userName, string profilePictureUrl, string email, string loginProvider, string providerKey,
        string providerName)
    {
        var user = new ApplicationUser
        {
            FirstName = firstName,
            LastName = lastName,
            UserName = userName,
            ProfilePictureUrl = profilePictureUrl,
            Email = email
        };

        var userExists = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);

        if (userExists == null)
        {
            var userCreated = await _userManager.CreateAsync(user);

            if (userCreated == null) return (Result.Failure(new List<string> { "Could not create user" }), "");
        }

        var result =
            await _userManager.AddLoginAsync(user, new UserLoginInfo(loginProvider, providerKey, providerName));

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<bool> UserExists(string email)
    {
        return await _userManager.Users.AnyAsync(x => x.Email == email);
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthenticateAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null) return false;

        var result = await _signInManager.PasswordSignInAsync(user, password, true, false);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> AddToRolesAsync(string userId, List<string> roles)
    {
        var administratorRole = new IdentityRole(Roles.Administrator);

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
            await _roleManager.CreateAsync(administratorRole);

        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);
        if (user == null) return Result.Failure(new List<string> { "User not found" });

        foreach (var role in roles)
        {
            var roleExist = await _roleManager.RoleExistsAsync(role);
            if (!roleExist) await _roleManager.CreateAsync(new IdentityRole(role));
        }

        var result = await _userManager.AddToRolesAsync(user, roles);

        return result.ToApplicationResult();
    }

    public async Task<Result> UpdatePasswordAsync(string userId, string currentPassword, string newPassword)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null) throw new NotFoundException("User not found");
        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        return result.ToApplicationResult();
    }

    public async Task<Result> UpdateEmailAsync(string userId, string email)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null) throw new NotFoundException("User not found");
        var token = await _userManager.GenerateChangeEmailTokenAsync(user, email);
        if (token == null) throw new NotFoundException("Invalid change email token");
        var result = await _userManager.ChangeEmailAsync(user, email, token);

        return result.ToApplicationResult();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }
}