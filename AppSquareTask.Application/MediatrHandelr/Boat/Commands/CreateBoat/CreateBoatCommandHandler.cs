using AppSquareTask.Application.IServices;
using AppSquareTask.Application.Responses;
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
using M = AppSquareTask.Core.Models;

namespace AppSquareTask.Application.MediatrHandelr.Boat.Commands.CreateBoat
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
			var course = _mapper.Map<M.Boat>(request);
			var createdBoat = await _boatService.CreateBoatAsync(course);
			var boatDto = _mapper.Map<BoatDto>(createdBoat);

			await _notificationService.NotifyAdminAsync($"A new owner has make an Boat: {request.OwnerId}");

			return _responseHandler.Created(boatDto);
		}
	}
}
