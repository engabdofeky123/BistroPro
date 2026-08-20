using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Admins
{
    public class PrintingPageData
    {
        public int TodayReservationsCount { get; set; } 
        public int ReservedTables { get; set; }
        public int AvailableTables { get; set; }

        public List<UpcomingReservation> UpcomingReservations { get; set; } = new List<UpcomingReservation>();
        public List<SpecialNote> SpecialNotes { get; set; } = new List<SpecialNote>();

        public string ManagerName { get; set; } 
        public DateTime GeneratedAt { get; set; }
        public string GeneratedBy { get; set; }

    }
}