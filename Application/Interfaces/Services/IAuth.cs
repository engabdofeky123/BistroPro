using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IAuth
    {
        Task<AuthResultMessge> Login(string email, string password, bool rememberMe);
        Task<AuthResultMessge> Register(string name, string email, string password);
        Task LogOut();
    }
}
