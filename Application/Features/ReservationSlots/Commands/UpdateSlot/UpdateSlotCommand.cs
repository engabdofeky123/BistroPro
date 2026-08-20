using Application.DTOs.ReservationsSlots;
using MediatR;

namespace Application.Features.ReservationSlots.Commands.UpdateSlot
{
    public record UpdateSlotCommand(ReservationSlotDto updated) : IRequest<AddOrUpdateOrDeleteSlotMessage>;
}
