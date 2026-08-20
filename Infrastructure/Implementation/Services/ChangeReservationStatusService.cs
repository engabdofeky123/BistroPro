using Application.Interfaces.Services;
using Azure.Core;
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
    public class ChangeReservationStatusService : IChangeReservationStatusService
    {
        private readonly ApplicationDbContext _context;

        public ChangeReservationStatusService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ChangeReservationStatusAsync(int id, ReservationStatus status)
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(
                    r => r.Id == id);

            if (reservation == null)
                return false;

            reservation.Status = status;
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
