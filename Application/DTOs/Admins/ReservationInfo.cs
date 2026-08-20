using Restaurant_Management.Models;

namespace Application.DTOs.Admins
{
    public class ReservationInfo
    {
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        public string CustomerName { get; set; }
        public DateTime ReservationTime { get; set; }
        public int TableNumber { get; set; }
        public int Guests { get; set; }
        public string? PhoneNNumber { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public ReservationStatus Status { get; set; }
    }
}
