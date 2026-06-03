namespace Restaurant_Management.ViewModels.Admins
{
    public class ScheduleViewModel
    {
        public int TotalGuests { get; set; }
        public int TotalReservations { get; set; }
        public float Occupancy { get; set; }
        public List<ReservationScheduleViewModel> Reservations { get; set; } = new(); 
    }
}