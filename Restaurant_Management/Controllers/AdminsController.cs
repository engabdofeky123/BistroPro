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
    }
}