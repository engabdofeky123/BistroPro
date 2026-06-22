using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Admins
{
    public class ScheduleDto
    {
        public int TotalGuests { get; set; }
        public int TotalReservations { get; set; }
        public float Occupancy { get; set; }
        public List<ReservationScheduleDto> Reservations { get; set; } = new();
    }
}
