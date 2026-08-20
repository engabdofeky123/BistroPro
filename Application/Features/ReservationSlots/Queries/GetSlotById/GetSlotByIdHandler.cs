using Application.DTOs.ReservationsSlots;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationSlots.Queries.GetSlotById
{
    public sealed class GetSlotByIdHandler : IRequestHandler<GetSlotByIdQuery, ReservationSlotDto>
    {
        private readonly ITimeSlotsRepository _timeSlotsRepository;

        public GetSlotByIdHandler(ITimeSlotsRepository repo)
        {
            _timeSlotsRepository = repo;
        }

        public async Task<ReservationSlotDto> Handle(GetSlotByIdQuery request, CancellationToken cancellationToken)
        {
            return await _timeSlotsRepository.GetById(request.slotId);
        }
    }
}
