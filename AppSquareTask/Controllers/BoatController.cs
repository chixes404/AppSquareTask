using AppSquareTask.Application.Dtos;
using AppSquareTask.Application.IServices;
using AppSquareTask.Core.MediatrHandelr.Boat.Commands;
using AppSquareTask.Core.MediatrHandelr.Boat.Queries;
using AppSquareTask.Core.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppSquareTask.Core.MediatrHandelr.Boat.Commands.CreateBoat;
using AppSquareTask.Core.MediatrHandelr.Boat.Queries.GetBoatById;
using AppSquareTask.Core.MediatrHandelr.Boat.Queries.GetBoatByOwner;
using AppSquareTask.Controllers.Base;
using AppSquareTask.Core.MediatrHandelr.Boat.Queries.GetAllBoats;
using AppSquareTask.Core.MediatrHandelr.AdminManagment.ApproveOwner;
using AppSquareTask.Core.MediatrHandelr.AdminManagment.RejectOwner;
using AppSquareTask.Core.MediatrHandelr.Boat.Commands.ApproveBoat;
using AppSquareTask.Core.MediatrHandelr.Boat.Commands.RejectBoat;
using AppSquareTask.Core.MediatrHandelr.Boat;
using Microsoft.AspNetCore.Authorization;

namespace AppSquareTask.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BoatsController : AppControllerBase
	{
		private readonly ApiResponseHandler _responseHandler;
		private readonly IMediator _mediator;

		public BoatsController(IMediator mediator, ApiResponseHandler responseHandler)
		{
			_mediator = mediator;
			_responseHandler = responseHandler;
		}


		[Authorize]

		[HttpGet]
		public async Task<IActionResult> GetAllBoatsPaginated([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
		{
			var boats = await _mediator.Send(new GetPaginatedBoatsQuery
			{
				PageNumber = pageNumber,
				PageSize = pageSize
			});

			var response = _responseHandler.Success(boats);
			return CreateResponse(response);
		}

		[Authorize("Owner , Admin")]

		[HttpPost("create")]
		public async Task<IActionResult> CreateBoat([FromBody] CreateBoatCommand command)
		{
			var result = await _mediator.Send(command);

			if (result == null)
			{
				var errorResponse = _responseHandler.BadRequest<BoatDto>("Failed to create boat");
				return CreateResponse(errorResponse);
			}

			var response = _responseHandler.Created(result);
			return CreateResponse(response);
		}


		[Authorize]

		[HttpGet("get-by-{id}")]
		public async Task<IActionResult> GetBoatById(int id)
		{
			var boat = await _mediator.Send(new GetBoatByIdQuery { Id = id });

			if (boat == null)
			{
				var errorResponse = _responseHandler.NotFound<BoatDto>($"Boat with ID {id} not found or not approved");
				return CreateResponse(errorResponse);
			}

			var response = _responseHandler.Success(boat);
			return CreateResponse(response);
		}

		[Authorize("Owner")]

		[HttpGet("get-by-Owner/{ownerId}")]
		public async Task<IActionResult> GetBoatsByOwner(int ownerId)
		{
			var boats = await _mediator.Send(new GetBoatByOwnerQuery { OwnerId = ownerId });

			if (boats == null)
			{
				var errorResponse = _responseHandler.NotFound<List<BoatDto>>($"No boats found for owner with ID {ownerId}");
				return CreateResponse(errorResponse);
			}

			var response = _responseHandler.Success(boats);
			return CreateResponse(response);
		}


		[Authorize("Admin")]

		[HttpPost("approve-boat/{boatId}")]
		public async Task<IActionResult> ApproveBoat(int boatId)
		{
			var result = await Mediator.Send(new ApproveBoatCommand { BoatId = boatId });
			return result.Succeeded ? Ok(result) : BadRequest(result);
		}
		[Authorize("Admin")]

		[HttpPost("reject-boat/{boatId}")]
		public async Task<IActionResult> RejectOBoat(int boatId)
		{
			var result = await Mediator.Send(new RejectBoatCommand { BoatId = boatId });
			return result.Succeeded ? Ok(result) : BadRequest(result);
		}

	
	}
}
