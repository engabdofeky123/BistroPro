using Application.DTOs.Admins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IGetAvailableTablesForReservationService
    {
        Task<List<AvailableTablesForReservations>> GetAvailableTablesForReservations(DateTime reservationDate, int numberOfGuests);
    }
}
