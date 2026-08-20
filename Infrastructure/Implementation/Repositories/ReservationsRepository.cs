using Application.DTOs.Admins;
using Application.DTOs.Reservations;
using Application.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Repositories
{
    public class ReservationsRepository : IReservationsRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservationsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DeleteReservationResult> DeleteReservationAsync(int id)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.Id == id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
                return new DeleteReservationResult { IsSuccess = true, Message = "Reservation deleted successfully." };
            }
            return new DeleteReservationResult { IsSuccess = false, Message = "Reservation not found." };
        }

        public async Task<ReservationDtoToUpdate?> GetReservationInfoAsync(int id)
        {
            return await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Table)
                .Where(r => r.Id == id).Select(r => new ReservationDtoToUpdate
                {
                    Id = r.Id,
                    CustomerName = r.Customer.Name,
                    ReservationTime = r.ReservationDate,
                    TableNumber = r.Table.TableNumber,
                    Guests = r.NumberOfGuests,
                    PhoneNNumber = r.Customer.PhoneNumber,
                    Status = r.Status,
                    Notes = r.Notes,
                    AvailableTables = _context.RestaurantTables
                        .Where(t => t.Capacity >= r.NumberOfGuests)
                        .Select(t => new AvailableTablesForReservations
                        {
                            TableNumber = t.TableNumber,
                            Capacity = t.Capacity
                        }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateReservationAsync(ReservationDtoToUpdate dto)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.Id == dto.Id);

            if (reservation == null)
                return false;

            var tableId = await _context.RestaurantTables
                .Where(t => t.TableNumber == dto.TableNumber)
                .Select(t => t.Id)
                .FirstOrDefaultAsync();

            if (tableId == 0)
                return false;

            reservation.Customer.Name = dto.CustomerName;
            reservation.Notes = dto.Notes;
            reservation.Customer.PhoneNumber = dto.PhoneNNumber;
            reservation.ReservationDate = dto.ReservationTime;
            reservation.NumberOfGuests = dto.Guests;
            reservation.Status = dto.Status;
            reservation.TableId = tableId;
            

            await _context.SaveChangesAsync();

            return true;
        }
    }
}