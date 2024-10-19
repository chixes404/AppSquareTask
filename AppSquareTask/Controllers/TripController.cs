using AppSquareTask.Application.Dtos;
using AppSquareTask.Application.IServices;
using AppSquareTask.Application.MediatrHandelr.Trip.Commands;
using AppSquareTask.Application.MediatrHandelr.Trip.Queries;
using AppSquareTask.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppSquareTask.Application.MediatrHandelr.Trip.Commands.CreateTrip;
using AppSquareTask.Application.MediatrHandelr.Trip.Queries.GetTripById;
using AppSquareTask.Application.MediatrHandelr.Trip.Queries.GetTripByOwner;
using AppSquareTask.Controllers.Base;
using AppSquareTask.Application.MediatrHandelr.Trip.Queries.GetAllTrips;
using Microsoft.AspNetCore.Authorization;

namespace AppSquareTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : AppControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITripService _tripService;

        public TripsController(IMediator mediator, ITripService tripService)
        {
            _mediator = mediator;
            _tripService = tripService;
        }

		//[Authorize(Roles = "Admin")]

		[HttpPost]
        public async Task<IActionResult> CreateTrip([FromBody] CreateTripCommand command)
        {
            var result = await _mediator.Send(command);

            if (result == null)
            {
                return BadRequest("Failed to create trip");
            }

            return Ok(result);
        }

		[Authorize(Roles = "Admin")]
		[HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrip(int id)
        {


            var result = await _mediator.Send(id);

            if (result == null)
            {
                return NotFound($"Trip with ID {id} not found");
            }

            return Ok(result);
        }


		[Authorize]
		[HttpGet("{id}")]
        public async Task<IActionResult> GetTripById(int id)
        {
            var trip = await _mediator.Send(new GetTripByIdQuery { Id = id });

            if (trip == null)
            {
                return NotFound($"Trip with ID {id} not found or not approved");
            }
            return Ok(trip);
        }

		[Authorize(Roles = "Admin , Owner")]
		[HttpGet("Owner/{ownerId}")]
        public async Task<IActionResult> GetTripsByOwner(int ownerId)
        {
            var trips = await _mediator.Send(new GetTripByOwnerQuery { OwnerId = ownerId });

            if (trips == null)
            {
                return NotFound($"No trips found for owner with ID {ownerId}");
            }

            return Ok(trips);
        }

		// GET: api/Trips

		[Authorize]

		[HttpGet]
        public async Task<IActionResult> GetAllTripsPaginated([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var trips = await _mediator.Send(new GetPaginatedTripsQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            });

            return Ok(trips);
        }
    }
}
