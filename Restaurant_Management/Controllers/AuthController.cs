using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Management.Identity;
using Restaurant_Management.ViewModels.AuthVM;

namespace Restaurant_Management.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly SignInManager<ApplicationUser> _SignInManager;

        public AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _SignInManager = signInManager;
            _UserManager = userManager;
        }

        [HttpGet]
        public IActionResult OpenLogin()
        {
            return View("LoginPage");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVm)
        {
            if (!ModelState.IsValid)
                return View("LoginPage", loginVm);

            var appUser = await _UserManager.FindByEmailAsync(loginVm.EmailAddress);

            if (appUser != null)
            {
                bool isFound = await _UserManager.CheckPasswordAsync(appUser, loginVm.Password);
                if (isFound)
                {
                    await _SignInManager.SignInAsync(appUser, loginVm.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Incorrect Email or password");
                return View("LoginPage", loginVm);
            }

            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                return View("LoginPage", loginVm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("OpenLogin");
        }
    }
}