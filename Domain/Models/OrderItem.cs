namespace Restaurant_Management.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MealId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        // Navigation properties
        public Order Order { get; set; }
        public Meal Meal { get; set; }
    }
}
