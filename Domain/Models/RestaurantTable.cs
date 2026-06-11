namespace Restaurant_Management.Models
{
    public class RestaurantTable
    {
        public int Id { get; set; }

        public int TableNumber { get; set; } 
        public int Capacity { get; set; }

        public bool IsAvailable { get; set; }

        // Navigation properties
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}