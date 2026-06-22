using Application.Features.Admins.Commands.AddNewTables;
using Application.Features.Admins.Queries.Dashboard;
using Application.Features.Admins.Queries.Reservations;
using Application.Features.Admins.Queries.Schedules;
using Application.Features.Admins.Queries.Tables;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Management.Models;
using Restaurant_Management.ViewModels.Admins;

namespace Restaurant_Management.Controllers
{
    public class AdminsController : Controller
    {
        private readonly IMediator _mediator;
        public AdminsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var query = new GetDashboardQuery();
            var dashboardData = await _mediator.Send(query);
            return View("AdminDashboard", dashboardData);
        }

        [HttpGet]
        public async Task<IActionResult> Reservations()
        {
            var query = new GetReservationsQuery();
            var result = await _mediator.Send(query);

            return View("Reservations", result);
        }

        [HttpGet]
        public async Task<IActionResult> Tables()
        {
            var query = new GetTablesQuery();
            var result = await _mediator.Send(query);

            return View("Tables", result);
        }

        [HttpGet]
        public async Task<IActionResult> Schedule()
        {
            var query = new GetScheduleeQuery(); 
            var result = await _mediator.Send(query);

            return View("Schedule", result);
        }

        [HttpGet]
        public IActionResult AddNewTable() => View("AddNewTable");

        [HttpPost]
        public async Task<IActionResult> SubmitAddNewTable(AddNewTableViewModel model) 
        {
            if (!ModelState.IsValid) 
                return View("AddNewTable", model);
            var command = new AddNewTableCommand(new Application.DTOs.Admins.AddNewTableDto
            {
                TableNumber = model.TableNumber,
                SeatingCapacity = model.SeatingCapacity,
                IsAvailable = model.IsAvailable
            });

            var result = await _mediator.Send(command);
            if (result.Success)
                return RedirectToAction("Tables");
            else
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View("AddNewTable", model);
            }
        }

    }
}