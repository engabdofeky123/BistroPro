using Application.DTOs.Admins;
using Application.Features.ReservationSlots.Queries;
using Application.Features.ReservationSlots.Queries.GetAllSlots;
using Application.Features.ReservationsOperations.Commands.ChangeReservationStatus;
using Application.Features.ReservationsOperations.Commands.DeleteReservation;
using Application.Features.ReservationsOperations.Commands.UpdateReservation;
using Application.Features.ReservationsOperations.Queries.FilterReservations;
using Application.Features.ReservationsOperations.Queries.GetReservationById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Management.Models;

namespace MVC.Controllers
{
    [Route("[controller]")]
    public class ReservationsController : Controller
    {
        private readonly IMediator _mediator;

        public ReservationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("update/{id:int}")]
        public async Task<IActionResult> Update(int id)
        {
            var command = new GetReservationByIdQuery(id);
            var model = await _mediator.Send(command);

            return View("Update", model);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitUpdate(ReservationDtoToUpdate model)
        {
            var command = new UpdateReservationCommand(model);
            var result = await _mediator.Send(command);

            if(!result)
            {
                ModelState.AddModelError("", "Failed to update the reservation.");
                return View("Update", model);
            }
            return RedirectToAction("Reservations", "Admins");
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteReservationCommand(id);

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
                message = "Reservation deleted successfully."
            });
        }

        

        
        // Get all reservation slots (Ajax)

        [HttpGet("slots")]
        public async Task<IActionResult> GetSlots()
        {
            var query = new GetAllReservationSlotsQuery();
            var slots = await _mediator.Send(query);

            return Ok(slots);
        }

        // Filter Reservations (Ajax)
        [HttpGet("FilterReservations")]
        public async Task<IActionResult> FilterReservations(string? status, DateTime? date)
        {
            var query = new FilterReservationsQuery(status, date);

            var filteredReservations = await _mediator.Send(query);

            return Json(filteredReservations);
        }

        [HttpPut("change-status")]
        public async Task<IActionResult> ChangeStatus( int reservationId, ReservationStatus status)
        {
            var command = new ChangeReservationStatusCommand(reservationId,status);

            var result = await _mediator.Send(command);

            if (!result)
            {
                return BadRequest(new
                {
                    message = "Failed to change reservation status."
                });
            }

            return Ok(new
            {
                message = "Reservation status updated successfully."
            });
        }
    }
}