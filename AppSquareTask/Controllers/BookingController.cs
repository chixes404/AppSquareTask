using AppSquareTask.Application.IServices;
using AppSquareTask.Application.MediatrHandelr.Booking.Commands.BookBoat;
using AppSquareTask.Application.MediatrHandelr.Booking.Commands.BookTrip;
using AppSquareTask.Application.MediatrHandelr.Booking.Commands.CancelBookingBoat;
using AppSquareTask.Application.MediatrHandelr.Booking.Commands.CancelBookingTrip;
using AppSquareTask.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AppSquareTask.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookingController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IBookingService _bookingService;

		public BookingController(IMediator mediator, IBookingService bookingService)
		{
			_mediator = mediator;
			_bookingService = bookingService;
		}
		[Authorize]

		[HttpPost("book-boat")]
		public async Task<ActionResult> BookBoat([FromBody] BookBoatCommand command)
		{
			var boatBooking = await _mediator.Send(command);
			if (boatBooking == null)
			{
				return BadRequest("Booking failed. Ensure the boat and user exist.");
			}

			return CreatedAtAction(nameof(_bookingService.GetBoatBookingByIdAsync), new { bookingId = boatBooking.Id }, boatBooking);
		}

		[Authorize]

		[HttpPost("cancel-boat")]
		public async Task<ActionResult> CancelBoatBooking([FromBody] CancelBookingBoatCommand command)
		{
			var result = await _mediator.Send(command);
			if (!result)
			{
				return NotFound("Booking not found or unable to cancel.");
			}
			return NoContent();
		}


		[Authorize]

		[HttpPost("book-trip")]
		public async Task<ActionResult> BookTrip([FromBody] BookTripCommand command)
		{
			var tripBooking = await _mediator.Send(command);
			if (tripBooking == null)
			{
				return BadRequest("Booking failed. Ensure the trip and user exist.");
			}

			return CreatedAtAction(nameof(_bookingService.GetBoatBookingByIdAsync), new { bookingId = tripBooking.Id }, tripBooking);
		}
		[Authorize]

		[HttpPost("cancel-trip")]
		public async Task<ActionResult> CancelTripBooking([FromBody] CancelBookingTripCommand command)
		{
			var result = await _mediator.Send(command);
			if (!result)
			{
				return NotFound("Booking not found or unable to cancel.");
			}
			return NoContent();
		}

		[Authorize]

		[HttpGet("boat/{bookingId}")]
		public async Task<ActionResult> GetBookingboatById(int bookingId)
		{
			var query = new GetBoatBookingByIdQuery(bookingId);
			var boatBooking = await _mediator.Send(query);

			if (boatBooking == null)
			{
				return NotFound();
			}

			return Ok(boatBooking);
		}

		[HttpGet("trip/{bookingId}")]
		public async Task<ActionResult> GetBookingtripById(int bookingId)
		{
			var query = new GetTripBookingByIdQuery(bookingId);
			var boatBooking = await _mediator.Send(query);

			if (boatBooking == null)
			{
				return NotFound();
			}

			return Ok(boatBooking);
		}
	}
}
