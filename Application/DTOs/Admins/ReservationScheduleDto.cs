using Restaurant_Management.Models;

namespace Application.DTOs.Admins
{
    public class ReservationScheduleDto
    {
        public string CustomerName { get; set; }

        public int TableNumber { get; set; }

        public int Guests { get; set; }

        public DateTime ReservationDate { get; set; }

        public ReservationStatus Status { get; set; }
    }
}
