using Application.DTOs.Admins;
using Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationsOperations.Queries.FilterReservations
{
    public sealed class FilterReservationsHandler : IRequestHandler<FilterReservationsQuery, List<ReservationInfo>>
    {
        private readonly IFilterReservationsService _filterReservationsService; 

        public FilterReservationsHandler(IFilterReservationsService filterReservationsService)
        {
            _filterReservationsService = filterReservationsService;
        }

        public async Task<List<ReservationInfo>> Handle(FilterReservationsQuery request, CancellationToken cancellationToken)
        {
            return await _filterReservationsService.FilterReservationsAsync(request.status, request.date);
        }
    }
}