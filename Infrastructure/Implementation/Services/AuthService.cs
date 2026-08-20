using Application.DTOs;

﻿using Application.DTOs.Auth;
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


        public async Task<AuthResultMessge> Register(RegisterDto dto)
        {
            var UserExists = await _userManager.FindByEmailAsync(dto.Email);
            if (UserExists != null)
                return new AuthResultMessge { IsAuthenticated = false, Message = "The Email is used " };

            var newUser = new ApplicationUser
            {
                Email = dto.Email,
                FullName = dto.FullName,
                UserName = dto.Email.Trim()
            };

            var result = await _userManager.CreateAsync(newUser,dto.Password);

            if(!result.Succeeded)
            {
                var errors = "";
                foreach (var error in result.Errors)
                {
                    errors += error.Description +", ";
                }
                return new AuthResultMessge
                {
                    IsAuthenticated = false,
                    Message = errors
                };
            }

             await _signInManager.SignInAsync(newUser, true);
            return new AuthResultMessge
            {
                IsAuthenticated = true,
                Message = "Logged In"
            };
            
        }
    }
}
