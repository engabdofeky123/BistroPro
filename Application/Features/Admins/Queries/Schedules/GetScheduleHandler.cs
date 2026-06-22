using Application.DTOs.Admins;
using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Admins.Queries.Schedules
{
    public class GetScheduleHandler : IRequestHandler<GetScheduleeQuery, ScheduleDto>
    {
        private readonly IAdminsRepository _adminRepository;
        public GetScheduleHandler(IAdminsRepository repo)
        {
            _adminRepository = repo;
        }
        public async Task<ScheduleDto> Handle(GetScheduleeQuery request, CancellationToken cancellationToken)
        {
            return await _adminRepository.GetScheduleData();
        }
    }
}