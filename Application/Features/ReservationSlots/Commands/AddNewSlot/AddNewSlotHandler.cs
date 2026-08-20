using Application.DTOs.ReservationsSlots;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationSlots.Commands.AddNewSlot
{
    public sealed class AddNewSlotHandler : IRequestHandler<AddNewSlotCommand, AddOrUpdateOrDeleteSlotMessage>
    {
        private readonly ITimeSlotsRepository _timeSlotsRepository;

        public AddNewSlotHandler(ITimeSlotsRepository repo)
        {
            _timeSlotsRepository = repo;
        }

        public async Task<AddOrUpdateOrDeleteSlotMessage> Handle(AddNewSlotCommand request, CancellationToken cancellationToken)
        {
            return await _timeSlotsRepository.AddNewSlot(request.newSlot);
        }
    }
}