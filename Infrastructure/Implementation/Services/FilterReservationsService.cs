using Application.DTOs.Admins;
using Application.Interfaces.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Restaurant_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services
{
    public class FilterReservationsService : IFilterReservationsService
    {
        private readonly ApplicationDbContext _context;

        public FilterReservationsService(ApplicationDbContext context)
        {
            _context = context;
        }

     
        public async Task<List<ReservationInfo>> FilterReservationsAsync( string? status, DateTime? date)
        {
            var reservationsQuery = _context.Reservations.AsNoTracking().AsQueryable();

            // Filter by Status
            if (!string.IsNullOrWhiteSpace(status))
            {
                if (Enum.TryParse<ReservationStatus>(
                    status,
                    true,
                    out var reservationStatus))
                {
                    reservationsQuery = reservationsQuery
                        .Where(r => r.Status == reservationStatus);
                }
            }

            // Filter by Date
            if (date.HasValue)
            {
                reservationsQuery = reservationsQuery
                    .Where(r => r.ReservationDate.Date == date.Value.Date);
            }


            return await reservationsQuery
                .OrderBy(r => r.ReservationDate)
                .Select(r => new ReservationInfo
                {
                    CustomerName = r.Customer.Name ?? "Unknown",
                    Guests = r.NumberOfGuests,
                    TableNumber = r.Table.TableNumber,
                    PhoneNNumber = r.Customer.PhoneNumber ?? "Unknown",
                    ReservationTime = r.ReservationDate,
                    Status = r.Status
                }).ToListAsync();
        }
    }
}