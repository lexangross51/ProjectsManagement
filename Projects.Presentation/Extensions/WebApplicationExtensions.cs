using Microsoft.AspNetCore.Identity;
using Projects.DataAccess.Database;
using Projects.Presentation.Models.Auth;

namespace Projects.Presentation.Extensions;

public static class WebApplicationExtensions
{
    public static async Task SeedRolesAndAdmin(this WebApplication app, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        var roleNames = new[] { Roles.Director, Roles.Manager, Roles.Employee };

        foreach (var role in roleNames)
        {
            bool isRoleExist = await roleManager.RoleExistsAsync(role);

            if (isRoleExist) continue;

            await roleManager.CreateAsync(new IdentityRole(role));
        }

        var admin = await userManager.FindByEmailAsync("admin@gmail.com");

        if (admin == null)
        {
            admin = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Admin",
                Email = "admin@gmail.com",
            };

            await userManager.CreateAsync(admin, "admin123");
        }
    }
}