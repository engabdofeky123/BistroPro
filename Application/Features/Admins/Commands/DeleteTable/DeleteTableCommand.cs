using Application.DTOs.Admins;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admins.Commands.DeleteTable
{
    public record DeleteTableCommand(int tableId) : IRequest<UpdateOrDeleteTableMessage>;
}
