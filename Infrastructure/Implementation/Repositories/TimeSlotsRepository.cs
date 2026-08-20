using Application.DTOs.ReservationsSlots;
using Application.Interfaces.Repositories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementation.Repositories
{
    public class TimeSlotsRepository : ITimeSlotsRepository
    {
        private readonly ApplicationDbContext _context;

        public TimeSlotsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AddOrUpdateOrDeleteSlotMessage> AddNewSlot(ReservationSlotDto newSlot)
        {
            var newSlotToSave = new ReservationSlot
            {
                StartTime = newSlot.StartTime,
                EndTime = newSlot.EndTime,
                IsActive = newSlot.IsActive,
            };

            await _context.ReservationSlots.AddAsync(newSlotToSave);
            await _context.SaveChangesAsync();

            return new AddOrUpdateOrDeleteSlotMessage
            {
                IsSuccessed = true,
                Message = "New Slot Added successfully"
            };
        }

        public async Task<AddOrUpdateOrDeleteSlotMessage> Delete(int id)
        {
            var slotToDelete = await _context.ReservationSlots.FirstOrDefaultAsync(x => x.Id == id);
            if (slotToDelete == null)
                return new AddOrUpdateOrDeleteSlotMessage { IsSuccessed = false, Message = "Slot Not Found" };

            _context.ReservationSlots.Remove(slotToDelete);
            await _context.SaveChangesAsync();
            return new AddOrUpdateOrDeleteSlotMessage
            {
                IsSuccessed = true,
                Message = "Slot Deleted successfully"
            };
        }

        public async Task<List<ReservationSlotDto>> GetAll()
        {
            return await _context.ReservationSlots.Select(x => new ReservationSlotDto
            {
                Id = x.Id,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                IsActive = x.IsActive

            }).ToListAsync();
        }

        public async Task<ReservationSlotDto?> GetById(int id)
        {
            var res = await _context.ReservationSlots.FirstOrDefaultAsync(x => x.Id == id);
            if (res == null)
                return null;
            return new ReservationSlotDto
            {
                Id = res.Id,
                IsActive = res.IsActive,
                EndTime = res.EndTime,
                StartTime = res.StartTime,
            };
        }

        public async Task<AddOrUpdateOrDeleteSlotMessage> UpdateSlot(ReservationSlotDto updatedSlot)
        {
            var existing = await _context.ReservationSlots
    .FirstOrDefaultAsync(x => x.Id == updatedSlot.Id);

            if (existing == null)
            {
                return new AddOrUpdateOrDeleteSlotMessage
                {
                    IsSuccessed = false,
                    Message = "Slot not found"
                };
            }

            existing.StartTime = updatedSlot.StartTime;
            existing.EndTime = updatedSlot.EndTime;
            existing.IsActive = updatedSlot.IsActive;

            await _context.SaveChangesAsync();

            return new AddOrUpdateOrDeleteSlotMessage
            {
                IsSuccessed = true,
                Message = "Slot updated successfully"
            };
        }
    } 
}