namespace Restaurant_Management.ViewModels.Admins
{
    public class AddNewTableViewModel
    {
        public int TableNumber { get; set; } 
        public int SeatingCapacity { get; set; } = 2;
        public bool IsAvailable { get; set; } = true;
    }
}