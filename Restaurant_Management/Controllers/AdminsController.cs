using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Management.Data;
using Restaurant_Management.Models;
using Restaurant_Management.ViewModels.Admins;
using System.Threading.Tasks;

namespace Restaurant_Management.Controllers
{
    public class AdminsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // dashboard calculations
            var dashboardData = new DashboardViewModel();

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

            return View("AdminDashboard", dashboardData);
        }

        [HttpGet]
        public async Task<IActionResult> Reservations()
        {
            var result = new ReservationViewModel();
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
            return View("Reservations", result);
        }

        [HttpGet]
        public async Task<IActionResult> Tables()
        {
            var result = new TablesViewModel();
            var restaurntTables = await _context.RestaurantTables.ToListAsync();
            result.TotalTables = restaurntTables.Count();
            result.AvailableTables = restaurntTables.Where(t => t.IsAvailable).Count();
            result.OccupiedTables = result.TotalTables - result.AvailableTables;

            result.Tables = restaurntTables.Select(t => new TableDataViewModel
            {
                IsAvailable = t.IsAvailable,
                Capacity = t.Capacity,
                TableNumber = t.TableNumber
            }).ToList();

            return View("Tables", result);
        }

        [HttpGet]
        public async Task<IActionResult> Schedule()
        {
            var result = new ScheduleViewModel();
            result.TotalReservations = await _context.Reservations.Where(r => r.ReservationDate == DateTime.Today).CountAsync();
            result.TotalGuests = await _context.Reservations.Where(r => r.ReservationDate == DateTime.Today).SumAsync(r => r.NumberOfGuests);
            result.Occupancy = result.TotalReservations > 0 ? (float)result.TotalGuests / (result.TotalReservations * 4) * 100 : 0; // Assuming each reservation is for a table of 4
            result.Reservations = await _context.Reservations.Include(r => r.Customer).Include(r => r.Table)
                .Where(r => r.ReservationDate.Date == DateTime.Today)
                .Select(res => new ReservationScheduleViewModel
                {
                    CustomerName = res.Customer.Name ?? "No Name",
                    Guests = res.NumberOfGuests,
                    ReservationDate = res.ReservationDate,
                    Status = res.Status,
                    TableNumber = res.Table.TableNumber
                }).ToListAsync();

            return View("Schedule", result);
        }

        [HttpGet]
        public IActionResult AddNewTable()
        {
            return View("AddNewTable");
        }

        [HttpPost]
        public async Task<IActionResult> AddNewTable(AddNewTableViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newTable = new RestaurantTable
                {
                    TableNumber = model.TableNumber,
                    Capacity = model.SeatingCapacity,
                    IsAvailable = model.IsAvailable
                };
                _context.RestaurantTables.Add(newTable);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Table added successfully.";
                RedirectToAction(nameof(Tables));
            }
            return View("AddNewTable", model);
        }
    }
}