using AppSquareTask.Application.Dtos;
using AppSquareTask.Application.IServices;
using AppSquareTask.Application.MediatrHandelr.Boat.Commands;
using AppSquareTask.Application.MediatrHandelr.Boat.Queries;
using AppSquareTask.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppSquareTask.Application.MediatrHandelr.Boat.Commands.CreateBoat;
using AppSquareTask.Application.MediatrHandelr.Boat.Queries.GetBoatById;
using AppSquareTask.Application.MediatrHandelr.Boat.Queries.GetBoatByOwner;
using AppSquareTask.Controllers.Base;
using AppSquareTask.Application.MediatrHandelr.Boat.Queries.GetAllBoats;

namespace AppSquareTask.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BoatsController : AppControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IBoatService _boatService;

		public BoatsController(IMediator mediator, IBoatService boatService)
		{
			_mediator = mediator;
			_boatService = boatService;
		}

		// POST: api/Boats
		[HttpPost]
		public async Task<IActionResult> CreateBoat([FromBody] CreateBoatCommand command)
		{
			var result = await _mediator.Send(command);

			if (result == null)
			{
				return BadRequest("Failed to create boat");
			}

			return Ok(result);
		}

		// PUT: api/Boats/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateBoat(int id)
		{
			

			var result = await _mediator.Send(id);

			if (result == null)
			{
				return NotFound($"Boat with ID {id} not found");
			}

			return Ok(result);
		}

	
		// GET: api/Boats/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetBoatById(int id)
		{
			var boat = await _mediator.Send(new GetBoatByIdQuery { Id = id });

			if (boat == null)
			{
				return NotFound($"Boat with ID {id} not found or not approved");
			}
			return Ok(boat);
		}

		// GET: api/Boats/Owner/{ownerId}
		[HttpGet("Owner/{ownerId}")]
		public async Task<IActionResult> GetBoatsByOwner(int ownerId)
		{
			var boats = await _mediator.Send(new GetBoatByOwnerQuery { OwnerId = ownerId });

			if (boats == null)
			{
				return NotFound($"No boats found for owner with ID {ownerId}");
			}

			return Ok(boats);
		}

		// GET: api/Boats
		[HttpGet]
		public async Task<IActionResult> GetAllBoatsPaginated([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
		{
			var boats = await _mediator.Send(new GetPaginatedBoatsQuery
			{
				PageNumber = pageNumber,
				PageSize = pageSize
			});

			return Ok(boats);
		}
	}
}
