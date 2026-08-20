using Application.DTOs.Admins;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationsOperations.Queries.FilterReservations
{
#nullable enable
    public record FilterReservationsQuery(string? status, DateTime? date) : IRequest<List<ReservationInfo>>;
}
