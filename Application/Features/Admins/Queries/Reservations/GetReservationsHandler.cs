using Application.DTOs.Admins;
using Application.Interfaces.Repositories;
using MediatR;


namespace Application.Features.Admins.Queries.Reservations
{
    internal class GetReservationsHandler : IRequestHandler<GetReservationsQuery, ReservationDto>
    {
        private readonly IAdminsRepository _adminRepository;

        public GetReservationsHandler (IAdminsRepository repo)
        {
            _adminRepository = repo;
        }

        public async Task<ReservationDto> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
        {
           return await _adminRepository.GetReservationsData();
        }
    }
}