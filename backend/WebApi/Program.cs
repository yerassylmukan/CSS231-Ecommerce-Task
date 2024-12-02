using Infrastructure;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnection")));

var app = builder.Build();

app.MapGet("api/", () => "Hello World!");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var identityContext = services.GetRequiredService<AppIdentityDbContext>();
        
        if (identityContext.Database.IsNpgsql())
        {
            identityContext.Database.Migrate();
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
}

app.Run();
