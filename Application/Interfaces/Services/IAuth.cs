using Application.DTOs;
﻿using Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IAuth
    {
        Task<AuthResultMessge> Register(RegisterDto dto);
        Task<AuthResultMessge> Login(string email, string password, bool rememberMe);
        Task LogOut();
    }
}
