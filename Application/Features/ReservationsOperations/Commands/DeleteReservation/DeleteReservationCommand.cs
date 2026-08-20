using Application.DTOs.Reservations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationsOperations.Commands.DeleteReservation
{
    public record DeleteReservationCommand(int id) : IRequest<DeleteReservationResult>;
}
