using Application.DTOs.Admins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IFilterReservationsService
    {
        Task<List<ReservationInfo>> FilterReservationsAsync(string? status, DateTime? date);
    }
}