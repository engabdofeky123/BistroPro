using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationsOperations.Commands.UpdateReservation
{
    public class UpdateReservationHandler : IRequestHandler<UpdateReservationCommand, bool>
    {
        private readonly IReservationsRepository _reservationRepository;

        public UpdateReservationHandler(IReservationsRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<bool> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            var result = await _reservationRepository.UpdateReservationAsync(request.dto);
            return result;
        }
    }
}
