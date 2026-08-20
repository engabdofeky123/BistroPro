using Application.DTOs.Admins;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationsOperations.Commands.UpdateReservation
{
    public record UpdateReservationCommand(ReservationDtoToUpdate dto) : IRequest<bool>;
}