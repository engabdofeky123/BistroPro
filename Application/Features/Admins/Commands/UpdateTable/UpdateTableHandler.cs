using Application.DTOs.Admins;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admins.Commands.UpdateTable
{
    public class UpdateTableHandler : IRequestHandler<UpdateTableCommand, UpdateOrDeleteTableMessage>
    {
        private readonly IAdminsRepository _adminsRepository;

        public UpdateTableHandler(IAdminsRepository adminsRepository)
        {
            _adminsRepository = adminsRepository;
        }

        public async Task<UpdateOrDeleteTableMessage> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
        {
            return await _adminsRepository.UpdateTable(request.model);
        }
    }
}
