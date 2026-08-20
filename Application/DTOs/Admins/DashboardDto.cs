using System;

﻿using Restaurant_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Admins
{
    public class DashboardDto
    {
        public int TodayReservations { get; set; }
        public int AvailableTables { get; set; }
        public int ReservedTables { get; set; }
        public int TotalCustomers { get; set; }

        public ICollection<ReservationInfo>? ReservationsOfToday { get; set; }  // Table of reservations for today
    }
}
