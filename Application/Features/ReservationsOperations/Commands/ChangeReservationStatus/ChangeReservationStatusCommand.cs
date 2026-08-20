using MediatR;
using Restaurant_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationsOperations.Commands.ChangeReservationStatus
{
    public record ChangeReservationStatusCommand(int id , ReservationStatus Status) : IRequest<bool>;
}
