using AppSquareTask.Application.IServices;
using AppSquareTask.Core.Responses;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.MediatrHandelr.Trip.Commands.UpdateTrip
{
	public class UpdateTripCommandHandler : IRequestHandler<UpdateTripCommand, ApiResponse<TripDto>>
	{
		private readonly ITripService _tripService;
		private readonly IMapper _mapper;
		private readonly ApiResponseHandler _responseHandler;

		public UpdateTripCommandHandler(ITripService tripService, IMapper mapper, ApiResponseHandler responseHandler)
		{
			_tripService = tripService;
			_mapper = mapper;
			_responseHandler = responseHandler;
		}


		public async Task<ApiResponse<TripDto>> Handle(UpdateTripCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var existingTrip = await _tripService.GetTripByIdAsync(request.Id);
				_mapper.Map(request, existingTrip);
				var updatedTrip = await _tripService.UpdateTripAsync(existingTrip);
				var tripDto = _mapper.Map<TripDto>(updatedTrip);
				return _responseHandler.Success(tripDto, "Trip updated successfully.");
			}
			catch (KeyNotFoundException)
			{
				return _responseHandler.NotFound<TripDto>($"Trip with ID {request.Id} not found.");
			}
		}
	}
}