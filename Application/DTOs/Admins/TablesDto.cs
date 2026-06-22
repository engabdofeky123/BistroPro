using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Admins
{
    public class TablesDto
    {
        public List<TableDataDto>? Tables { get; set; }
        public int TotalTables { get; set; }
        public int AvailableTables { get; set; }
        public int OccupiedTables { get; set; }
        public int TotalCapacity { get; set; }
    }
}
