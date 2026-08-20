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
    internal class PrintDailyBreifService : IPrintDailyBreifService
    {
        private readonly ApplicationDbContext _context;

        public PrintDailyBreifService(ApplicationDbContext context)
        {
            _context = context;
        }


        public Task<byte[]> GeneratePdfAsync(PrintingPageData data)
        {
            throw new NotImplementedException();
        }

        public async Task<PrintingPageData> GetPrintingPageDataAsync()
        {
            return new PrintingPageData
            {
                AvailableTables = await _context.RestaurantTables.CountAsync(t => t.IsAvailable),

                ReservedTables = await _context.RestaurantTables.CountAsync(t => !t.IsAvailable),

                ManagerName = "Abdo Mohammed",

                TodayReservationsCount = await _context.Reservations.CountAsync(r => r.ReservationDate.Date == DateTime.Today), 

                UpcomingReservations = await _context.Reservations
            .Where(r => r.ReservationDate.Date == DateTime.Today)
            .Include(r => r.Table)
            .Include(r=> r.Customer)
            .OrderBy(r => r.ReservationDate)
            .Take(5)
            .Select(r => new UpcomingReservation
            {
                CustomerName = r.Customer.Name?? "NO NAME SPECIFIED"!,
                TableNumber = r.Table.TableNumber,
                NumberOfGuests = r.NumberOfGuests,
                ReservationDate = r.ReservationDate
            }).ToListAsync(),

                SpecialNotes = await _context.Reservations.Where(r =>
                    r.ReservationDate.Date == DateTime.Today &&
                    !string.IsNullOrWhiteSpace(r.Notes))
            .Select(r => new SpecialNote
            {
                Name = r.Customer.Name! ?? "----",
                TableNumber = r.Table.TableNumber,
                Note = r.Notes
            }).ToListAsync()
            };
        }
    }
}