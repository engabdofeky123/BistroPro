using Application.DTOs.Admins;
using Application.Features.Admins.Queries.GetAvailableTablesForAddReservationForm;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using MediatR;


namespace Application.Features.ReservationsOperations.Queries.GetReservationById
{
    public class GetReservationByIdHandler : IRequestHandler<GetReservationByIdQuery, ReservationDtoToUpdate>
    {
        private readonly IReservationsRepository _reservationRepository;
        public GetReservationByIdHandler(IReservationsRepository reservationRepository, IGetAvailableTablesForReservationService getAvailableTablesForReservationService)
        {
            _reservationRepository = reservationRepository;
        }
        public async Task<ReservationDtoToUpdate> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            return await _reservationRepository.GetReservationInfoAsync(request.Id);
            
        }   
    }
}
