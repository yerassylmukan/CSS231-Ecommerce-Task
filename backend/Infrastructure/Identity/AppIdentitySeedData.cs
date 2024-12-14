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
        await EnsureRoleExistsAsync(roleManager, "ProductManager");
        await EnsureRoleExistsAsync(roleManager, "InventoryManager");
        await EnsureRoleExistsAsync(roleManager, "OrderManager");
        await EnsureRoleExistsAsync(roleManager, "UserManager");

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

        var productUserName = "productmanager@gmail.com";
        var productUser = new ApplicationUser
        {
            UserName = productUserName,
            Email = productUserName,
            FirstName = "Product",
            LastName = "Manager",
            ProfilePictureUrl = "urlToPhoto"
        };

        if (await userManager.FindByEmailAsync(productUserName) == null)
        {
            await userManager.CreateAsync(productUser, "Product@2424");
            await userManager.AddToRoleAsync(productUser, "ProductManager");
        }

        var inventoryUserName = "inventorymanager@gmail.com";
        var inventoryUser = new ApplicationUser
        {
            UserName = inventoryUserName,
            Email = inventoryUserName,
            FirstName = "Inventory",
            LastName = "Manager",
            ProfilePictureUrl = "urlToPhoto"
        };

        if (await userManager.FindByEmailAsync(inventoryUserName) == null)
        {
            await userManager.CreateAsync(inventoryUser, "Inventory@2424");
            await userManager.AddToRoleAsync(inventoryUser, "InventoryManager");
        }

        var orderUserName = "ordermanager@gmail.com";
        var orderUser = new ApplicationUser
        {
            UserName = orderUserName,
            Email = orderUserName,
            FirstName = "Order",
            LastName = "Manager",
            ProfilePictureUrl = "urlToPhoto"
        };

        if (await userManager.FindByEmailAsync(orderUserName) == null)
        {
            await userManager.CreateAsync(orderUser, "Order@2424");
            await userManager.AddToRoleAsync(orderUser, "OrderManager");
        }

        var userManagerUserName = "usermanager@gmail.com";
        var userManagerUser = new ApplicationUser
        {
            UserName = userManagerUserName,
            Email = userManagerUserName,
            FirstName = "User",
            LastName = "Manager",
            ProfilePictureUrl = "urlToPhoto"
        };

        if (await userManager.FindByEmailAsync(userManagerUserName) == null)
        {
            await userManager.CreateAsync(userManagerUser, "UserMngr@2424");
            await userManager.AddToRoleAsync(userManagerUser, "UserManager");
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