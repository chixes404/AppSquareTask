using AppSquareTask.Application.IServices;
using AppSquareTask.Core.Responses;
using AppSquareTask.Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using M = AppSquareTask.Data.Models;
using AppSquareTask.Data.Models;

namespace AppSquareTask.Core.MediatrHandelr.Boat.Commands.CreateBoat
{
	public class CreateBoatCommandHandler : IRequestHandler<CreateBoatCommand, ApiResponse<BoatDto>>
	{
		private readonly IBoatService _boatService;
		private readonly IMapper _mapper;
		private readonly ApiResponseHandler _responseHandler;
		private readonly INotificationService _notificationService;


		public CreateBoatCommandHandler(IBoatService boatService, IMapper mapper, ApiResponseHandler responseHandler, INotificationService notificationService)
		{
			_boatService = boatService;
			_mapper = mapper;
			_responseHandler = responseHandler;
			_notificationService = notificationService;
		}

		public async Task<ApiResponse<BoatDto>> Handle(CreateBoatCommand request, CancellationToken cancellationToken)
		{

		var boat = _mapper.Map<M.Boat>(request);
			boat.Status = Status.Pending;
			

			var createdBoat = await _boatService.CreateBoatAsync(boat);
			var boatDto = _mapper.Map<BoatDto>(createdBoat);

			await _notificationService.NotifyAdminAsync($"A new owner has make an Boat: {request.OwnerId}");

			return _responseHandler.Created(boatDto);
		}
	}
}
