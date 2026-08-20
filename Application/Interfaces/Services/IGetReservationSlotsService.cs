using Application.DTOs.ReservationsSlots;


namespace Application.Interfaces.Services
{
    public interface IGetReservationSlotsService
    {
        Task<List<ReservationSlotDto>> GetReservationSlotsAsync();
    }
}