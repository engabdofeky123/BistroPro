using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ReservationsSlots
{
    public class AddOrUpdateOrDeleteSlotMessage 
    {
        public bool IsSuccessed { get; set; }
        public string Message { get; set; } 
    }
}
