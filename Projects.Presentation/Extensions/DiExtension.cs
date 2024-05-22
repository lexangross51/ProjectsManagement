using Microsoft.AspNetCore.Identity;
using Projects.DataAccess.Database;

namespace Projects.Presentation.Extensions;

public static class DiExtension
{
    public static IServiceCollection AddIdentityUser(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        
        services.AddScoped<RoleManager<IdentityRole>>();
        services.AddScoped<UserManager<ApplicationUser>>();
        services.AddScoped<SignInManager<ApplicationUser>>();
        
        return services;
    }
}