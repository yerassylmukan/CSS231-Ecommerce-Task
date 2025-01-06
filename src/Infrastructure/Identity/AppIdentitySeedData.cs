using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity;

public class AppIdentitySeedData
{
    public static async Task SeedAsync(AppIdentityDbContext dbContext, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        if (dbContext.Database.IsNpgsql()) dbContext.Database.Migrate();

        await EnsureRoleExistsAsync(roleManager, "Admin");
        await EnsureRoleExistsAsync(roleManager, "BasicUser");
        await EnsureRoleExistsAsync(roleManager, "Anonymous");

        var adminUserName = "admin@gmail.com";
        var adminUser = new ApplicationUser
        {
            UserName = adminUserName,
            Email = adminUserName,
            FirstName = "Admin",
            LastName = "Admin",
            ProfilePictureUrl = "urlToPhoto"
        };

        if (await userManager.FindByEmailAsync(adminUserName) == null)
        {
            await userManager.CreateAsync(adminUser, "Admin@2424");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        var user1UserName = "user1@gmail.com";
        var user1 = new ApplicationUser
        {
            UserName = user1UserName,
            Email = user1UserName,
            FirstName = "User1",
            LastName = "BasicUser",
            ProfilePictureUrl = "urlToPhoto"
        };

        if (await userManager.FindByEmailAsync(user1UserName) == null)
        {
            await userManager.CreateAsync(user1, "User1@2424");
            await userManager.AddToRoleAsync(user1, "BasicUser");
        }

        var user2UserName = "user2@gmail.com";
        var user2 = new ApplicationUser
        {
            UserName = user2UserName,
            Email = user2UserName,
            FirstName = "User2",
            LastName = "BasicUser",
            ProfilePictureUrl = "urlToPhoto"
        };

        if (await userManager.FindByEmailAsync(user2UserName) == null)
        {
            await userManager.CreateAsync(user2, "User2@2424");
            await userManager.AddToRoleAsync(user2, "BasicUser");
        }
    }

    private static async Task EnsureRoleExistsAsync(RoleManager<IdentityRole> roleManager, string roleName)
    {
        if (!await roleManager.RoleExistsAsync(roleName)) await roleManager.CreateAsync(new IdentityRole(roleName));
    }
}