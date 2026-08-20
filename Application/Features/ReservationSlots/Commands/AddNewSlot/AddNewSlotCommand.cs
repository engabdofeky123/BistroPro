using Application.DTOs.ReservationsSlots;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationSlots.Commands.AddNewSlot
{
    public record AddNewSlotCommand(ReservationSlotDto newSlot) : IRequest<AddOrUpdateOrDeleteSlotMessage>;
}