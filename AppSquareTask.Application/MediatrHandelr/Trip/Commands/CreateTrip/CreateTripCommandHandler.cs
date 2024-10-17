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

namespace AppSquareTask.Application.MediatrHandelr.Trip.Commands.CreateTrip
{
	public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, ApiResponse<TripDto>>
	{
		private readonly ITripService _tripService;
		private readonly IMapper _mapper;
		private readonly ApiResponseHandler _responseHandler;
		private readonly INotificationService _notificationService;


		public CreateTripCommandHandler(ITripService tripService, IMapper mapper, ApiResponseHandler responseHandler, INotificationService notificationService)
		{
			_tripService = tripService;
			_mapper = mapper;
			_responseHandler = responseHandler;
			_notificationService = notificationService;
		}

		public async Task<ApiResponse<TripDto>> Handle(CreateTripCommand request, CancellationToken cancellationToken)
		{
			var course = _mapper.Map<M.Trip>(request);
			var createdTrip = await _tripService.CreateTripAsync(course);
			var tripDto = _mapper.Map<TripDto>(createdTrip);

			await _notificationService.NotifyAdminAsync($"A new owner has make an Trip: {request.OwnerId}");

			return _responseHandler.Created(tripDto);
		}
	}
	}
