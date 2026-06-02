using Restaurant_Management.Models;

namespace Restaurant_Management.ViewModels.Admins
{
    public class ReservationInfo
    {
        public string CustomerName { get; set; }
        public DateTime ReservationTime { get; set; }
        public int TableNumber { get; set; }
        public int Guests { get; set; }
        public string? PhoneNNumber { get; set; }
        public ReservationStatus Status { get; set; }
    }
}