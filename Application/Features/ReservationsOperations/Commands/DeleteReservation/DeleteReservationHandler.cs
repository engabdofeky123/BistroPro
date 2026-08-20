using Application.DTOs.Reservations;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationsOperations.Commands.DeleteReservation
{
    public class DeleteReservationHandler : IRequestHandler<DeleteReservationCommand, DeleteReservationResult>
    {
        private readonly IReservationsRepository _reservationsRepository;

        public DeleteReservationHandler(IReservationsRepository reservationsRepository)
        {
            _reservationsRepository = reservationsRepository;
        }

        public async Task<DeleteReservationResult> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            return await _reservationsRepository.DeleteReservationAsync(request.id);
        }
    }
}
