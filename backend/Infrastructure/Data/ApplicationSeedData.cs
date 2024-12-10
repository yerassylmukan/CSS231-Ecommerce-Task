using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationSeedData
{
    public static void Seed(ApplicationDbContext dbContext)
    {
        if (dbContext.Database.IsNpgsql()) dbContext.Database.Migrate();
    }
}