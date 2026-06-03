using Restaurant_Management.Models;

namespace Restaurant_Management.ViewModels.Admins
{
    public class ReservationScheduleViewModel
    {
        public string CustomerName { get; set; }

        public int TableNumber { get; set; }

        public int Guests { get; set; }

        public DateTime ReservationDate { get; set; }

        public ReservationStatus Status { get; set; }
    }
}