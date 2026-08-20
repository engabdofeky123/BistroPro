using Application.DTOs.Admins;
using Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admins.Queries.GetAvailableTablesForAddReservationForm
{
    public class GetAvailableTablesForAddReservationHandler : IRequestHandler<GetAvailableTablesForAddReservationQuery, List<AvailableTablesForReservations>>
    {
        private IGetAvailableTablesForReservationService _getAvailableTablesService;
        public GetAvailableTablesForAddReservationHandler(IGetAvailableTablesForReservationService service)
        {
            _getAvailableTablesService = service;
        }

        public async Task<List<AvailableTablesForReservations>> Handle(GetAvailableTablesForAddReservationQuery request, CancellationToken cancellationToken)
        {
            return await _getAvailableTablesService.GetAvailableTablesForReservations(request.reservationDate, request.numberOfGuests);
        }
    }
}
