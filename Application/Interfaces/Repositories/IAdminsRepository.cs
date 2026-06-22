using Application.DTOs.Admins;
using Restaurant_Management.Models;


namespace Application.Interfaces.Repositories
{
    public interface IAdminsRepository
    {
        Task<DashboardDto> GetDashboardData();
        Task<ReservationDto> GetReservationsData();
        Task<TablesDto> GetTablesData();
        Task<ScheduleDto> GetScheduleData();
        Task<AddNewTableMessage> AddNewTable(RestaurantTable newTable);
    }
}