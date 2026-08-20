using Application.DTOs.Admins;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admins.Commands.DeleteTable
{
    public class DeleteTableHandler : IRequestHandler<DeleteTableCommand, UpdateOrDeleteTableMessage>
    {
        private readonly IAdminsRepository _adminsRepository;

        public DeleteTableHandler(IAdminsRepository repo)
        {
            _adminsRepository = repo;   
        }

        public async Task<UpdateOrDeleteTableMessage> Handle(DeleteTableCommand request, CancellationToken cancellationToken)
        {
            return await _adminsRepository.DeleteTable(request.tableId);
        }
    }
}
