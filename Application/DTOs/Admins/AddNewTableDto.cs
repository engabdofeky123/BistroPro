using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Admins
{
    public class AddNewTableDto
    {
        public int TableNumber { get; set; }
        public int SeatingCapacity { get; set; } = 2;
        public bool IsAvailable { get; set; } = true;
    }
}
