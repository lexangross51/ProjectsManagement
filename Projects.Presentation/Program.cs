using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projects.DataAccess;
using Projects.DataAccess.Database;
using Projects.Logic;
using Projects.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services
    .AddDatabase(cfg => cfg.UseSqlite(connectionString))
    .AddLogic()
    .AddMemoryCache()
    .AddIdentityUser()
    .ConfigureApplicationCookie(options => options.LoginPath = "/Auth/Login")
    .AddControllersWithViews();

// builder.Services.AddAuthorizationBuilder()
//     .AddPolicy("RequireManagerRole", policy => policy.RequireRole(Roles.Manager))
//     .AddPolicy("RequireEmployeeRole", policy => policy.RequireRole(Roles.Employee));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    _ = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    app.SeedRolesAndAdmin(roleManager, userManager).Wait();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();