using Restaurant_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Admins
{
    public class ReservationDtoToUpdate
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime ReservationTime { get; set; }
        public int TableNumber { get; set; }
        public int Guests { get; set; }
        public string? PhoneNNumber { get; set; }
        public string? Notes { get; set; } = string.Empty;
        public ReservationStatus Status { get; set; }
        public List<AvailableTablesForReservations> AvailableTables { get; set; } = new List<AvailableTablesForReservations>();

    }
}
