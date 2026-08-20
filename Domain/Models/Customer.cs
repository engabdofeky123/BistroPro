
namespace Restaurant_Management.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string? UserId { get; set; } 
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        // Navigation properties
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}