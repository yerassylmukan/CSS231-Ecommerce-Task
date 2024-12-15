using System.Text.Json;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly IDistributedCache _cache;
    private readonly IEmailSender _emailSender;
    private readonly ILogger<IdentityService> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenClaimsService _tokenClaimsService;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(IDistributedCache cache, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
        SignInManager<ApplicationUser> signInManager, ITokenClaimsService tokenClaimsService, IEmailSender emailSender,
        ILogger<IdentityService> logger)
    {
        _cache = cache;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _tokenClaimsService = tokenClaimsService;
        _emailSender = emailSender;
        _logger = logger;
    }

    public async Task<string> CreateUserAsync(string email, string password, string firstName, string lastName,
        string profilePictureUrl)
    {
        var userExists = await _userManager.Users.AnyAsync(u => u.Email == email);
        if (userExists) throw new UserAlreadyExistsException(email);

        var user = new ApplicationUser
        {
            Email = email,
            UserName = email,
            FirstName = firstName,
            LastName = lastName,
            ProfilePictureUrl = profilePictureUrl
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"User creation failed: {errors}");
        }

        await _userManager.AddToRoleAsync(user, "BasicUser");

        await _signInManager.PasswordSignInAsync(user, password, false, false);

        var token = await _tokenClaimsService.GetTokenAsync(user.UserName);
        return token;
    }

    public async Task<string> AuthenticateUserAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email)
                   ?? throw new UserNotFoundException(email);

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded) throw new IncorrectPasswordException(email);

        return await _tokenClaimsService.GetTokenAsync(user.UserName);
    }

    public string AuthenticateAnonymousUser()
    {
        return _tokenClaimsService.GetAnonymousToken();
    }

    public async Task<string> AddUserToRolesAsync(string email, IEnumerable<string> roles)
    {
        var user = await _userManager.FindByEmailAsync(email)
                   ?? throw new UserNotFoundException(email);

        foreach (var role in roles)
        {
            var roleExist = await _roleManager.RoleExistsAsync(role);
            if (!roleExist) throw new RoleDoesNotExistsException(role);

            await _userManager.AddToRoleAsync(user, role);
        }

        var newToken = await _tokenClaimsService.GetTokenAsync(user.UserName);

        return newToken;
    }

    public async Task SendPasswordResetTokenAsync(string email, string linkToResetPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            throw new UserNotFoundException(email);

        Random r = new Random();
        int code = r.Next(100000, 999999);
        
        var resetData = new PasswordResetData
        {
            Code = code,
            Email = email
        };

        var cacheKey = $"PasswordReset:{email}";
        var expiration = TimeSpan.FromMinutes(15); // Set code expiration time.

        await _cache.SetStringAsync(
            cacheKey,
            JsonSerializer.Serialize(resetData),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            });

        _logger.LogInformation($"Password reset code: {code}");

        await _emailSender.EmailSendAsync(
            email,
            "Password Reset Request",
            $"To reset your password, click the link: {linkToResetPassword}. Your code is {code}",
            CancellationToken.None
        );
    }

    public async Task ResetPasswordAsync(string email, int code, string newPassword)
    {
        var cacheKey = $"PasswordReset:{email}";
        var cachedData = await _cache.GetStringAsync(cacheKey);

        if (cachedData == null)
            throw new ArgumentException("Password reset code expired or not found.");

        var jsonElement = JsonSerializer.Deserialize<JsonElement>(cachedData);

        if (!jsonElement.TryGetProperty("Code", out var codeProperty) || codeProperty.GetInt32() != code)
            throw new InvalidOperationException("Invalid password reset code.");

        var user = await _userManager.FindByEmailAsync(email);
        
        if (user == null)
            throw new UserNotFoundException(email);

        await _userManager.RemovePasswordAsync(user);
        await _userManager.AddPasswordAsync(user, newPassword);

        _logger.LogInformation("Password reset successful for user {Email}", email);

        await _cache.RemoveAsync(cacheKey);
    }
}