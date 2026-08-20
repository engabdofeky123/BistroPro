using Application.DTOs.Admins;
using Application.Interfaces.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services
{
    public class GetAvailableTablesForReservationService : IGetAvailableTablesForReservationService
    {
        private readonly ApplicationDbContext _context;

        public GetAvailableTablesForReservationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<AvailableTablesForReservations>> GetAvailableTablesForReservations(DateTime reservationDate, int numberOfGuests)
        {
            var reservedTableIds = await _context.Reservations
                .Where(r => r.ReservationDate == reservationDate)
                .Select(r => r.TableId).ToListAsync();

            return await _context.RestaurantTables.Where(t =>
                t.IsAvailable &&
                t.Capacity >= numberOfGuests &&
                !reservedTableIds.Contains(t.Id))
                .Select(t => new AvailableTablesForReservations
                 {
                    TableNumber = t.TableNumber,
                    Capacity = t.Capacity
                 }).ToListAsync();

        }
    }
}
