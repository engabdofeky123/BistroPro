using Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, bool>
    {
        private readonly IAuth _auth;
        public LogoutCommandHandler(IAuth auth)
        {
            _auth = auth;
        }
        public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
             await _auth.LogOut();
            return true;
        }
    }
}
