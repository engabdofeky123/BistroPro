using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Admins
{ 
    public class ReservationDto 
    {
        public int TodayReservations { get; set; }
        public int PendingRequests { get; set; }
        public List<ReservationInfo> ReservationsOfToday { get; set; }
    }
}
