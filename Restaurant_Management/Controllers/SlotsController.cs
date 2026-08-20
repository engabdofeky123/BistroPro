using Application.DTOs.ReservationsSlots;
using Application.Features.ReservationSlots.Commands.AddNewSlot;
using Application.Features.ReservationSlots.Commands.DeleteSlot;
using Application.Features.ReservationSlots.Commands.UpdateSlot;
using Application.Features.ReservationSlots.Queries.GetAllSlots;
using Application.Features.ReservationSlots.Queries.GetSlotById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    [Route("[controller]")]
    public class SlotsController : Controller
    {
        private readonly IMediator _mediator;

        public SlotsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            var query = new GetAllReservationSlotsQuery();
            var result = await _mediator.Send(query);

            return View("Index",result);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpGet("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var query = new GetSlotByIdQuery(id);
            var slot = await _mediator.Send(query);
            if(slot == null)
                return NotFound("Slot not found");

            return View("Create", slot);
        }

        [HttpPost("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ReservationSlotDto updated)       
        {
            var command = new UpdateSlotCommand(updated);
            var result = await _mediator.Send(command);

            if (!result.IsSuccessed)
                return NotFound(result.Message);
            return RedirectToAction("Index");
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteSlotCommand(id);

            var result = await _mediator.Send(command);

            return Json(new
            {
                success = result.IsSuccessed,
                message = result.Message
            });
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ReservationSlotDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var command = new AddNewSlotCommand(dto);
            var result = await _mediator.Send(command);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);

            return RedirectToAction(nameof(Index));
        }
    }
}