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
            result.PendingRequests = await _context.Reservations.Where(r => r.Status == ReservationStatus.Pending).CountAsync();
            result.TodayReservations = await _context.Reservations.Where(r => r.ReservationDate.Date == DateTime.Today).CountAsync();

            result.ReservationsOfToday = await _context.Reservations.Include(r => r.Customer).Include(r => r.Table)
                .Where(r => r.ReservationDate.Date == DateTime.Today)
                .Select(res => new ReservationInfo
                {
                    CustomerName = res.Customer.Name ?? "No Name",
                    Guests = res.NumberOfGuests,
                    ReservationTime = res.ReservationDate,
                    Status = res.Status,
                    TableNumber = res.Table.TableNumber,
                    PhoneNNumber = res.Customer.PhoneNumber
                }).ToListAsync();
            return result;
        }

        public async Task<ScheduleDto> GetScheduleData()
        {
            var result = new ScheduleDto();
            result.TotalReservations = await _context.Reservations.Where(r => r.ReservationDate == DateTime.Today).CountAsync();
            result.TotalGuests = await _context.Reservations.Where(r => r.ReservationDate == DateTime.Today).SumAsync(r => r.NumberOfGuests);
            result.Occupancy = result.TotalReservations > 0 ? (float)result.TotalGuests / (result.TotalReservations * 4) * 100 : 0; // Assuming each reservation is for a table of 4
            result.Reservations = await _context.Reservations.Include(r => r.Customer).Include(r => r.Table)
                .Where(r => r.ReservationDate.Date == DateTime.Today)
                .Select(res => new ReservationScheduleDto 
                { 
                    CustomerName = res.Customer.Name ?? "No Name",
                    Guests = res.NumberOfGuests,
                    ReservationDate = res.ReservationDate,
                    Status = res.Status,
                    TableNumber = res.Table.TableNumber
                }).ToListAsync();
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
                TableNumber = t.TableNumber
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
    }
}