using ApplicationCore.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity;

public class AppIdentitySeedData
{
    public static async Task SeedAsync(AppIdentityDbContext dbContext, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        if (dbContext.Database.IsNpgsql())
        {
            dbContext.Database.Migrate();
        }

        await EnsureRoleExistsAsync(roleManager, "Admin");
        await EnsureRoleExistsAsync(roleManager, "BasicUser");
        await EnsureRoleExistsAsync(roleManager, "ProductManager");
        await EnsureRoleExistsAsync(roleManager, "OrderManager");
        await EnsureRoleExistsAsync(roleManager, "UserManager");

        string adminUserName = "admin@gmail.com";
        var adminUser = new ApplicationUser
        {
            UserName = "admin",
            Email = "admin@gmail.com",
            FirstName = "Admin",
            LastName = "Admin",
            ProfilePictureUrl = "urlToPhoto"
        };

        if (await userManager.FindByEmailAsync(adminUserName) == null)
        {
            await userManager.CreateAsync(adminUser, "Admin@2424");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }

    private static async Task EnsureRoleExistsAsync(RoleManager<IdentityRole> roleManager, string roleName)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

}