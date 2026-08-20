using Application.DTOs.ReservationsSlots;
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
    public class GetReservationSlotsService : IGetReservationSlotsService
    {
        private readonly ApplicationDbContext _context;

        public GetReservationSlotsService(ApplicationDbContext context)
        {
            _context = context;
        }

   

        public async Task<List<ReservationSlotDto>> GetReservationSlotsAsync()
        {
            return await _context.ReservationSlots.Select(x => new ReservationSlotDto
            {
                Id = x.Id,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                IsActive = x.IsActive,
            }).ToListAsync();
        }
    }
}