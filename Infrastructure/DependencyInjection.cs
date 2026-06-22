using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Infrastructure.Data;
using Infrastructure.Implementation.Repositories;
using Infrastructure.Implementation.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant_Management.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
      
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();



            services.AddScoped<IAuth, AuthService>();
            services.AddScoped<IAdminsRepository, AdminsRepository>();
            return services;
        }
    }
}