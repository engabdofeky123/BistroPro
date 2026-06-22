using Application.Features.Auth.Login;
using Application.Features.Auth.Logout;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Management.Identity;
using Restaurant_Management.ViewModels.AuthVM;

namespace Restaurant_Management.Controllers
{
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediatR)
        {
            _mediator = mediatR;
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

            var cmd = new LoginCommand(loginVm.EmailAddress,loginVm.Password,loginVm.RememberMe);
            var result = await _mediator.Send(cmd);

            if (result.IsAuthenticated)
                return RedirectToAction("Index", "Admins");
            else
                ModelState.AddModelError("", result.Message!);
            return View("LoginPage", loginVm);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var cmd = new LogoutCommand();
            await _mediator.Send(cmd);
            return RedirectToAction("LoginPage");
        }
    }
}