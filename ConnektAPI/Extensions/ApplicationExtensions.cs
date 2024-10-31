using ConnektAPI_Core.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ConnektAPI.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure DbContext first
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                migrations => migrations.MigrationsAssembly(typeof(Program).Assembly.GetName().Name)));
        return services;
    }

    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        // Configure DbContext first
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}