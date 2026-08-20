using Application.DTOs.Admins;
using Application.DTOs.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IReservationsRepository
    {
        Task<ReservationDtoToUpdate> GetReservationInfoAsync(int id);
        Task<bool> UpdateReservationAsync(ReservationDtoToUpdate dto);
        Task<DeleteReservationResult> DeleteReservationAsync(int id);
    }
}
