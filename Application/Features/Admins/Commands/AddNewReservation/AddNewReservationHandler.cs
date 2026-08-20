using Application.DTOs.Admins;
using Application.Interfaces.Repositories;
using MediatR;
using Restaurant_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admins.Commands.AddNewReservation
{
    public class AddNewReservationHandler : IRequestHandler<AddNewReservationCommand, AddNewReservationMessage>
    {
        private readonly IAdminsRepository _adminsRepository;

        public AddNewReservationHandler(IAdminsRepository repo)
        {
            _adminsRepository = repo;
        }

        public async Task<AddNewReservationMessage> Handle(AddNewReservationCommand request, CancellationToken cancellationToken)
        {
            return await _adminsRepository.AddNewReservation(request.input);
        }
    }
}
