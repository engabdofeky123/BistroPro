using Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationsOperations.Commands.ChangeReservationStatus
{
    public sealed class ChangeReservationStatusHandler : IRequestHandler<ChangeReservationStatusCommand, bool>
    {
        private readonly IChangeReservationStatusService _changeReservationStatusService;

        public ChangeReservationStatusHandler(IChangeReservationStatusService service)
        {
            _changeReservationStatusService = service;
        }

        public async Task<bool> Handle(ChangeReservationStatusCommand request, CancellationToken cancellationToken)
        {
            return await _changeReservationStatusService.ChangeReservationStatusAsync(request.id, request.Status);
        }
    }
}