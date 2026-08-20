using Application.DTOs;
﻿using Application.DTOs.Auth;
using MediatR;

namespace Application.Features.Auth.Login
{
    public record LoginCommand(string email, string password , bool remember) : IRequest<AuthResultMessge>;
}
