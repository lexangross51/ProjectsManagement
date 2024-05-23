using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projects.DataAccess.Database;
using Projects.Presentation.Models.Auth;

namespace Projects.Presentation.Controllers;

public class AuthController(SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager) : Controller
{
    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        try
        {
            var user = await userManager.FindByNameAsync(dto.UserName);

            if (user == null)
            {
                ModelState.AddModelError(nameof(dto.UserName), "This user does not exist");
                return View(dto);
            }
            
            var result = await signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(nameof(dto.Password), "Invalid password");

            return View(dto);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }
}