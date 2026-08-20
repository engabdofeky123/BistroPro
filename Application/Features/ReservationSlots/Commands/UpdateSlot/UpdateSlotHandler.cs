using Application.DTOs.ReservationsSlots;
using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Features.ReservationSlots.Commands.UpdateSlot
{
    public sealed class UpdateSlotHandler : IRequestHandler<UpdateSlotCommand, AddOrUpdateOrDeleteSlotMessage>
    {
        private readonly ITimeSlotsRepository _timeSlotsRepository;

        public UpdateSlotHandler(ITimeSlotsRepository repo)
        {
            _timeSlotsRepository = repo;
        }

        public async Task<AddOrUpdateOrDeleteSlotMessage> Handle(UpdateSlotCommand request, CancellationToken cancellationToken)
        {
            return await _timeSlotsRepository.UpdateSlot(request.updated);
        }
    }
}