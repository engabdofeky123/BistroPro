namespace Application.DTOs.Admins
{
    public class UpcomingReservation
    {
        public string CustomerName { get; set; }
        public DateTime ReservationDate { get; set; }
        public int TableNumber { get; set; }
        public int NumberOfGuests { get; set; }
    }
}
