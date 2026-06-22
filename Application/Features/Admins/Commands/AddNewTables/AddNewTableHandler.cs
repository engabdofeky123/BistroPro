using Application.DTOs.Admins;
using Application.Interfaces.Repositories;
using MediatR;
using Restaurant_Management.Models;


namespace Application.Features.Admins.Commands.AddNewTables
{
    public class AddNewTableHandler : IRequestHandler<AddNewTableCommand, AddNewTableMessage>
    {
        private readonly IAdminsRepository _adminsRepository;
        public AddNewTableHandler(IAdminsRepository repo)
        {
            _adminsRepository = repo;
        }
        public async Task<AddNewTableMessage> Handle(AddNewTableCommand request, CancellationToken cancellationToken)
        {
            var table = new RestaurantTable
            {
                TableNumber = request.dto.TableNumber,
                Capacity = request.dto.SeatingCapacity,
                IsAvailable = request.dto.IsAvailable
            };

            return await _adminsRepository.AddNewTable(table);
        }
    }
}