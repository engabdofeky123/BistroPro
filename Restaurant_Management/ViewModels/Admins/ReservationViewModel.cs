namespace Restaurant_Management.ViewModels.Admins
{
    public class ReservationViewModel
    {
        public int TodayReservations { get; set; }
        public int PendingRequests { get; set; }
        public List<ReservationInfo> ReservationsOfToday { get; set; } 
    }
}