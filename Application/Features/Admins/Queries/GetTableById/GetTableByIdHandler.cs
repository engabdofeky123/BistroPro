using Application.DTOs.Admins;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admins.Queries.GetTableById
{
    public class GetTableByIdHandler : IRequestHandler<GetTableByIdQuery, UpdateTableDto>
    {
        private readonly IAdminsRepository _adminsRepository;

        public GetTableByIdHandler(IAdminsRepository repo)
        {
            _adminsRepository = repo;
        }

        public async Task<UpdateTableDto> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
        {
            var table = await _adminsRepository.GetTableById(request.tableId);
            return new UpdateTableDto
            {
                Id = table.Id,
                TableNumber = table.TableNumber,
                SeatingCapacity = table.Capacity,
                IsAvailable = table.IsAvailable
            };  
        }
    }
}