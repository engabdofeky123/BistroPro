using Application.DTOs.Admins;
using Application.Features.Admins.Commands.AddNewReservation;
using Application.Features.Admins.Commands.AddNewTables;
using Application.Features.Admins.Commands.DeleteTable;
using Application.Features.Admins.Commands.UpdateTable;
using Application.Features.Admins.Queries.Dashboard;
using Application.Features.Admins.Queries.GetAvailableTablesForAddReservationForm;
using Application.Features.Admins.Queries.GetTableById;
using Application.Features.Admins.Queries.Reservations;
using Application.Features.Admins.Queries.Schedules;
using Application.Features.Admins.Queries.Tables;
using Application.Features.ReservationSlots.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using Restaurant_Management.ViewModels.Admins;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task<IActionResult> Schedule(DateTime? date)
        {
            var selectedDate = date ?? DateTime.Today;

            var query = new GetScheduleeQuery(selectedDate);

            var result = await _mediator.Send(query);

            return View("Schedule", result);
        }


        // Tables Management Endpoints
        [HttpGet]
        public IActionResult AddNewTable() => View("AddNewTable");

        [HttpGet]
        public IActionResult DeleteTable() => View("DeleteTable");

        [HttpGet]
        public async Task<IActionResult> UpdateTable([FromQuery] int id) 
        {
            var query = new GetTableByIdQuery(id);
            var result = await _mediator.Send(query);
            return View("UpdateTable",result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitUpdateTable(UpdateTableDto model) 
        {
            if (!ModelState.IsValid) 
                return View("UpdateTable", model);
            var command = new UpdateTableCommand(model);
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return RedirectToAction("Tables");
            else
            {
                ModelState.AddModelError(string.Empty, result.Message!);
                return View("UpdateTable", model);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTable(int tableId)
        {
            var command = new DeleteTableCommand(tableId);

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(new
                {
                    message = result.Message
                });
            }

            return Ok(new
            {
                message = result.Message
            });
        }

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


        // Reservations Management Endpoints

        [HttpGet]
        public IActionResult AddNewReservation() => View("AddNewReservation");

        [HttpPost]
        public async Task<IActionResult> SubmitAddNewReservation(AddNewReservationDto model)
        {
            if (!ModelState.IsValid)
                return View("AddNewReservation", model);

            var command = new AddNewReservationCommand(model);
            var result = await _mediator.Send(command);

            if (result.Success)
                return RedirectToAction("Reservations");
            else
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View("AddNewReservation", model);
            }
        }

        [HttpGet]  // used in ajax call to get available tables for a specific date and number of guests
        public async Task<IActionResult> GetAvailableTablesForAddReservationForm(DateTime reservationDate, int numberOfGuests)
        {
            var query = new GetAvailableTablesForAddReservationQuery(reservationDate, numberOfGuests);
            var availableTables = await _mediator.Send(query);
            return Json(availableTables);
        }

        [HttpGet("daily-brief")]
        public async Task<IActionResult> PrintDailyBrief(DateTime? date)
        {
            var selectedDate = date ?? DateTime.Today;

            var query = new GetScheduleeQuery(selectedDate);

            var result = await _mediator.Send(query);

            return View("PrintDailyBrief", result);

        }


        [HttpGet]
        public async Task<IActionResult> FilterSchedule(DateTime date)
        {
            var query = new GetScheduleeQuery(date);
            var result = await _mediator.Send(query);

            return Json(result);
        }
    }
}