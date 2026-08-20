using Application.DTOs.Admins;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admins.Queries.GetTableById
{
    public record GetTableByIdQuery(int tableId) : IRequest<UpdateTableDto>;
}
