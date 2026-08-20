using Application.DTOs.Admins;
using Restaurant_Management.Models;


namespace Application.Interfaces.Repositories
{
    public interface IAdminsRepository
    {
        Task<DashboardDto> GetDashboardData();
        Task<ReservationDto> GetReservationsData();
        Task<TablesDto> GetTablesData();
        Task<ScheduleDto> GetScheduleData(DateTime date);
        Task<AddNewTableMessage> AddNewTable(RestaurantTable newTable);
        Task<UpdateOrDeleteTableMessage> DeleteTable(int tableId);
        Task<RestaurantTable> GetTableById(int tableId);
        Task<UpdateOrDeleteTableMessage> UpdateTable(UpdateTableDto updatedTable);
        Task<AddNewReservationMessage> AddNewReservation(AddNewReservationDto newReservation);
    }
}