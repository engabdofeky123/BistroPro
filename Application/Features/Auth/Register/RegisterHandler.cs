using Application.DTOs.Auth;
using Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Register
{
    public sealed class RegisterHandler : IRequestHandler<RegisterCommand, AuthResultMessge>
    {
        private readonly IAuth authService;

        public RegisterHandler(IAuth service)
        {
            authService = service;
        }
     
        public async Task<AuthResultMessge> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await authService.Register(request.dto);

        }
    }
}
