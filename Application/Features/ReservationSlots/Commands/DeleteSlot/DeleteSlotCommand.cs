using Application.DTOs.ReservationsSlots;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationSlots.Commands.DeleteSlot
{
    public record DeleteSlotCommand(int id) : IRequest<AddOrUpdateOrDeleteSlotMessage>;
}