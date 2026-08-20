using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Admins
{
    public class AddNewReservationDto
    {
        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public DateTime ReservationDate { get; set; }

        public int ReservationSlotId { get; set; }

        public int NumberOfGuests { get; set; }

        public int TableNumber { get; set; }

#nullable enable
        public string? Notes { get; set; }

        public List<AvailableTablesForReservations> AvailableTables { get; set; }
            = new List<AvailableTablesForReservations>();
    }

}