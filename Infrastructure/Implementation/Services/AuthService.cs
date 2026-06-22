using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Restaurant_Management.Identity;

namespace Infrastructure.Implementation.Services
{
    public class AuthService : IAuth
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<AuthResultMessge> Login(string email, string password, bool rememberMe)
        {
            var appUser = await _userManager.FindByEmailAsync(email);

            if (appUser != null)
            {
                bool isFound = await _userManager.CheckPasswordAsync(appUser, password);
                if (isFound)
                {
                    await _signInManager.SignInAsync(appUser, rememberMe);
                    return new AuthResultMessge
                    {
                        IsAuthenticated = true,
                        Message = "Login Successful",
                        UserName = appUser.UserName,
                        Password = password,
                        Email = appUser.Email
                    };
                }
                return new AuthResultMessge 
                {
                    IsAuthenticated = false,
                    Message = "Invalid Login Attempt",
                    UserName = appUser.UserName
                };            
            }

            else
                return new AuthResultMessge 
                {
                    Message = "Invalid Login Attempt",
                };
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public Task<AuthResultMessge> Register(string name, string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
