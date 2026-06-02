namespace Restaurant_Management.ViewModels.Admins
{
    public class TablesViewModel
    {
        public List<TableDataViewModel> Tables { get; set; }
        public int TotalTables { get; set; }    
        public int AvailableTables { get; set; }
        public int OccupiedTables { get; set; }
        public int TotalCapacity { get; set; }
    }

    public class  TableDataViewModel
    {
        public int TableNumber { get; set; }

        public int Capacity { get; set; }

        public bool IsAvailable { get; set; }
    }
}
