using Application.DTOs.ReservationsSlots;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationSlots.Queries.GetSlotById
{
    public record GetSlotByIdQuery(int slotId) : IRequest<ReservationSlotDto>;
}
