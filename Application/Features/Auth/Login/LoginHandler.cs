using Application.DTOs;
﻿using Application.DTOs.Auth;
using Application.Interfaces.Services;
using MediatR;

namespace Application.Features.Auth.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, AuthResultMessge>
    {
        private readonly IAuth _auth;
        public LoginHandler(IAuth auth)
        {
            _auth = auth;
        }
        public async Task<AuthResultMessge> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _auth.Login(request.email, request.password, request.remember);
        }
    }
}