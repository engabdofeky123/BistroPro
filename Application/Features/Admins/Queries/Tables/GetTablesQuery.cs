using Application.DTOs.Admins;
using MediatR;


namespace Application.Features.Admins.Queries.Tables
{
    public record GetTablesQuery() : IRequest<TablesDto>;
}