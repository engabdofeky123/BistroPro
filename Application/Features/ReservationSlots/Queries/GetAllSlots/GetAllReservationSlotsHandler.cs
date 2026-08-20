using Application.DTOs.ReservationsSlots;
using Application.Interfaces.Services;
using MediatR;

namespace Application.Features.ReservationSlots.Queries.GetAllSlots
{
    public sealed class GetAllReservationSlotsHandler : IRequestHandler<GetAllReservationSlotsQuery, List<ReservationSlotDto>>
    {
        private readonly IGetReservationSlotsService _getReservationSlotsService;

        public GetAllReservationSlotsHandler(IGetReservationSlotsService service)
        {
            _getReservationSlotsService = service;
        }

        public async Task<List<ReservationSlotDto>> Handle(GetAllReservationSlotsQuery request, CancellationToken cancellationToken)
        {
            return await _getReservationSlotsService.GetReservationSlotsAsync();
        }
    }
}