
using Restaurant_Management.Models;

namespace Restaurant_Management.ViewModels.Admins
{
    public class DashboardViewModel
    {
        public int TodayReservations { get; set; }
        public int AvailableTables { get; set; }
        public int ReservedTables { get; set; }
        public int TotalCustomers { get; set; }

        public ICollection<ReservationInfo> ReservationsOfToday { get; set; }  // Table of reservations for today

    }
}
