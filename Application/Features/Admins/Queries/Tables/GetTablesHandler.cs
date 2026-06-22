using Application.DTOs.Admins;
using Application.Interfaces.Repositories;
using MediatR;


namespace Application.Features.Admins.Queries.Tables
{
    public class GetTablesHandler : IRequestHandler<GetTablesQuery, TablesDto>
    {
        private readonly IAdminsRepository _adminRepository;
        public GetTablesHandler(IAdminsRepository repo)
        {
            _adminRepository = repo;
        }
        public async Task<TablesDto> Handle(GetTablesQuery request, CancellationToken cancellationToken)
        {
            return await _adminRepository.GetTablesData();
        }
    }
}