namespace Restaurant_Management.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TableId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int NumberOfGuests { get; set; }
        // Navigation properties
        public Customer Customer { get; set; }
        public ReservationStatus Status { get; set; }
        public RestaurantTable Table { get; set; }
    }
}
