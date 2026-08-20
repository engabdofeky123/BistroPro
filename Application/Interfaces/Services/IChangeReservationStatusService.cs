using Restaurant_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IChangeReservationStatusService
    {
        Task<bool> ChangeReservationStatusAsync(int id , ReservationStatus status); 
    }
}
