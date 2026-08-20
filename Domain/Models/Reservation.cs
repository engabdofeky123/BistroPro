using Domain.Models;

namespace Restaurant_Management.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public DateTime ReservationDate { get; set; }

        public int ReservationSlotId { get; set; }

        public ReservationSlot ReservationSlot { get; set; }

        public int NumberOfGuests { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public int TableId { get; set; }

        public RestaurantTable Table { get; set; }

        public ReservationStatus Status { get; set; }

        public string Notes { get; set; }   
    }
}
