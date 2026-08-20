using Application.DTOs.Admins;
using Application.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Restaurant_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Repositories
{
    public class AdminsRepository : IAdminsRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<DashboardDto> GetDashboardData()
        {
            var dashboardData = new DashboardDto();

            dashboardData.TotalCustomers = await _context.Customers.CountAsync();
            dashboardData.AvailableTables = await _context.RestaurantTables.Where(t => t.IsAvailable).CountAsync();
            dashboardData.ReservedTables = await _context.RestaurantTables.Where(t => !t.IsAvailable).CountAsync();
            dashboardData.TodayReservations = await _context.Reservations.Where(r => r.ReservationDate.Date == DateTime.Today).CountAsync();
            dashboardData.ReservationsOfToday = await _context.Reservations.Include(r => r.Customer).Include(r => r.Table)
                .Where(r => r.ReservationDate.Date == DateTime.Today)
                .Select(res => new ReservationInfo
                {
                    CustomerName = res.Customer.Name ?? "No Name",
                    Guests = res.NumberOfGuests,
                    ReservationTime = res.ReservationDate,
                    Status = res.Status,
                    TableNumber = res.Table.TableNumber
                }).ToListAsync();
            return dashboardData;
        }

        public async Task<ReservationDto> GetReservationsData()
        {
            var result = new ReservationDto();

            result.PendingRequests = await _context.Reservations
                .CountAsync(r => r.Status == ReservationStatus.Pending);

            result.TodayReservations = await _context.Reservations
                .CountAsync(r => r.ReservationDate.Date == DateTime.Today);

            result.ReservationsOfToday = await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Table)
                .Include(r => r.ReservationSlot)
                .OrderBy(r => r.ReservationSlot.StartTime)
                .Select(res => new ReservationInfo
                {
                    ReservationId = res.Id,
                    CustomerName = res.Customer.Name ?? "No Name",
                    Guests = res.NumberOfGuests,
                    ReservationTime = res.ReservationDate,
                    StartTime = res.ReservationSlot.StartTime,
                    EndTime = res.ReservationSlot.EndTime,
                    Status = res.Status,
                    TableNumber = res.Table.TableNumber,
                    PhoneNNumber = res.Customer.PhoneNumber
                })
                .ToListAsync();

            return result;
        }

        public async Task<ScheduleDto> GetScheduleData(DateTime date)
        {
            var selectedDate = date.Date;

            var result = new ScheduleDto();

            result.TotalReservations = await _context.Reservations
                .CountAsync(r => r.ReservationDate.Date == selectedDate);

            result.TotalGuests = await _context.Reservations
                .Where(r => r.ReservationDate.Date == selectedDate)
                .SumAsync(r => r.NumberOfGuests);

            result.Occupancy = result.TotalReservations > 0
                ? (float)result.TotalGuests / (result.TotalReservations * 4) * 100
                : 0;

            result.Reservations = await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Table)
                .Where(r => r.ReservationDate.Date == selectedDate)
                .Select(res => new ReservationScheduleDto
                {
                    CustomerName = res.Customer.Name ?? "No Name",
                    Guests = res.NumberOfGuests,
                    ReservationDate = res.ReservationDate,
                    Status = res.Status,
                    TableNumber = res.Table.TableNumber
                })
                .ToListAsync();

            return result;
        }

        public async Task<TablesDto> GetTablesData()
        {
            var result = new TablesDto();
            var restaurntTables = await _context.RestaurantTables.ToListAsync();
            result.TotalTables = restaurntTables.Count();
            result.AvailableTables = restaurntTables.Where(t => t.IsAvailable).Count();
            result.OccupiedTables = result.TotalTables - result.AvailableTables;

            result.Tables = restaurntTables.Select(t => new TableDataDto
            {
                IsAvailable = t.IsAvailable,
                Capacity = t.Capacity,
                TableNumber = t.TableNumber,
                Id = t.Id
            }).ToList();
            return result;
        }
     
        public async Task<AddNewTableMessage> AddNewTable(RestaurantTable newTable)
        {
            var isFound =  _context.RestaurantTables.Any(x=> x.TableNumber == newTable.TableNumber);
            if (isFound)
                return new AddNewTableMessage { Success = false , Message = "Same Table number exists!" };
            await _context.RestaurantTables.AddAsync(newTable);
            await _context.SaveChangesAsync();
            return new AddNewTableMessage { Success = true, Message = "New Table added successfully!" };
        }

        public async Task<AddNewReservationMessage> AddNewReservation(AddNewReservationDto newReservation)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(x =>
                    x.PhoneNumber == newReservation.CustomerPhone);

            if (customer == null)
            {
                customer = new Customer
                {
                    Name = newReservation.CustomerName,
                    PhoneNumber = newReservation.CustomerPhone
                };

                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
            }

            var table = await _context.RestaurantTables
                .FirstOrDefaultAsync(x =>
                    x.TableNumber == newReservation.TableNumber);

            if (table == null)
                return new AddNewReservationMessage
                {
                    Success = false,
                    Message = "Table not found"
                };

            var reservationExists = await _context.Reservations
                .AnyAsync(x =>
                    x.Customer.PhoneNumber == newReservation.CustomerPhone &&
                    x.ReservationDate == newReservation.ReservationDate);

            if (reservationExists)
                return new AddNewReservationMessage
                {
                    Success = false,
                    Message = "Same reservation already exists"
                };

            var tableReserved = await _context.Reservations
               .AnyAsync(x =>
                   x.TableId == table.Id &&
                   x.ReservationDate == newReservation.ReservationDate);

            if (tableReserved)
                return new AddNewReservationMessage
                {
                    Success = false,
                    Message = "Table is already reserved for the selected date and time"
                };

            var reservation = new Reservation
            {
                CustomerId = customer.Id,
                TableId = table.Id,
                ReservationDate = newReservation.ReservationDate,
                NumberOfGuests = newReservation.NumberOfGuests,
                Notes = newReservation.Notes!,
                ReservationSlotId = newReservation.ReservationSlotId
            };

            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();

            return new AddNewReservationMessage
            {
                Success = true,
                Message = "Reservation added successfully"
            };
        }

        public async Task<UpdateOrDeleteTableMessage> DeleteTable(int tableId)
        {
            var table = await _context.RestaurantTables.FirstOrDefaultAsync(x => x.Id == tableId);
            if (table == null)
               return new UpdateOrDeleteTableMessage { IsSuccess = false, Message = "Table not found!" };

            var hasReservations = await _context.Reservations.AnyAsync(r => r.TableId == tableId);
            if (hasReservations)
                return new UpdateOrDeleteTableMessage
                {
                    IsSuccess = false,
                    Message = "Cannot delete this table because it has reservations."
                };

            _context.RestaurantTables.Remove(table);
            await _context.SaveChangesAsync();
            return new UpdateOrDeleteTableMessage
            {
                IsSuccess = true,
                Message = "Table Deleted Successfully"
            };
        }

        public async Task<UpdateOrDeleteTableMessage> UpdateTable(UpdateTableDto updatedTable)
        {
            var table = await GetTableById(updatedTable.Id);
            if (table == null)
                return new UpdateOrDeleteTableMessage { IsSuccess = false, Message = "Table not found!" };

            table.TableNumber = updatedTable.TableNumber;
            table.Capacity = updatedTable.SeatingCapacity;
            table.IsAvailable = updatedTable.IsAvailable;
            await _context.SaveChangesAsync();

            return new UpdateOrDeleteTableMessage { IsSuccess = true, Message = "Table Updated Successfully" };
        }
        public async Task<RestaurantTable> GetTableById(int tableId) => await _context.RestaurantTables?.FirstOrDefaultAsync(x => x.Id == tableId);
    }
}