using Application.DTOs.Admins;
using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Admins.Queries.Dashboard
{
    public class GetDashboardHandler : IRequestHandler<GetDashboardQuery, DashboardDto>
    {
        private readonly IAdminsRepository _adminRepository;

        public GetDashboardHandler(IAdminsRepository repo)
        {
            _adminRepository = repo;
        }

        public async Task<DashboardDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            return await _adminRepository.GetDashboardData();
        }
    }
}