using Application.DTOs.ReservationsSlots;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationSlots.Commands.DeleteSlot
{
    public sealed class DeleteSlotHandler : IRequestHandler<DeleteSlotCommand, AddOrUpdateOrDeleteSlotMessage>
    {
        private readonly ITimeSlotsRepository _timeSlotsRepository;

        public DeleteSlotHandler(ITimeSlotsRepository repo)
        {
            _timeSlotsRepository = repo;
        }

        public async Task<AddOrUpdateOrDeleteSlotMessage> Handle(DeleteSlotCommand request, CancellationToken cancellationToken)
        {
            return await _timeSlotsRepository.Delete(request.id);
        }
    }
}