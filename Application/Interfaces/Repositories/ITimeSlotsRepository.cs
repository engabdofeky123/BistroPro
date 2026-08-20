using Application.DTOs.ReservationsSlots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ITimeSlotsRepository
    {
        Task<List<ReservationSlotDto>> GetAll();
        Task<ReservationSlotDto> GetById(int id);
        Task<AddOrUpdateOrDeleteSlotMessage> UpdateSlot(ReservationSlotDto updatedSlot); 
        Task<AddOrUpdateOrDeleteSlotMessage> Delete(int id);    
        Task<AddOrUpdateOrDeleteSlotMessage> AddNewSlot(ReservationSlotDto newSlot); 
    }
}
