using System.Text;
using System.Text.Json.Serialization;
using ApplicationCore.Constants;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApi;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// To ensure that webapi successfully connects to the databases (db and identity),
// I set a delay of 5 seconds to avoid errors during migration.
// Docker depends_on does not wait for dependent containers to be fully ready to accept connections.
await Task.Delay(5000);

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

builder.Services.AddScoped<ITokenClaimsService, TokenClaimService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();
builder.Services.AddScoped<ICatalogItemService, CatalogItemService>();
builder.Services.AddScoped<ICatalogItemReviewService, CatalogItemReviewService>();
builder.Services.AddScoped<ICatalogTypeService, CatalogTypeService>();
builder.Services.AddScoped<ICatalogBrandService, CatalogBrandService>();


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();

var key = Encoding.ASCII.GetBytes(JwtSettings.KEY);
builder.Services.AddAuthentication(config =>
    {
        config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(config =>
    {
        config.RequireHttpsMetadata = false;
        config.SaveToken = false;
        config.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["REDIS_CONNECTION"];
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddLogging();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.EnableAnnotations();
    c.SchemaFilter<CustomSchemaFilters>();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddCors(options =>
    options.AddPolicy("CorsPolicy", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader();
    }));

var app = builder.Build();

app.Logger.LogInformation("Web API created...");

app.Logger.LogInformation("Seeding Database...");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var identityContext = services.GetRequiredService<AppIdentityDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        var applicationDbContext = services.GetRequiredService<ApplicationDbContext>();

        await AppIdentitySeedData.SeedAsync(identityContext, userManager, roleManager);
        await ApplicationSeedData.SeedAsync(applicationDbContext, app.Logger);
    }
    catch (Exception e)
    {
        app.Logger.LogError(e, "An error occurred while seeding the database.");
    }
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();

app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

app.MapControllers();

app.Run();

public partial class Program
{
}