using Application.DTOs;
using MediatR;

namespace Application.Features.Auth.Login
{
    public record LoginCommand(string email, string password , bool remember) : IRequest<AuthResultMessge>;
}
