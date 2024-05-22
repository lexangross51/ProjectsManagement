using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projects.DataAccess.Database;
using Projects.Presentation.Models.Auth;

namespace Projects.Presentation.Controllers;

public class AuthController(SignInManager<ApplicationUser> signInManager) : Controller
{
    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        try
        {
            var result = await signInManager.PasswordSignInAsync(dto.UserName, dto.Password, dto.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt");

            return View(dto);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}