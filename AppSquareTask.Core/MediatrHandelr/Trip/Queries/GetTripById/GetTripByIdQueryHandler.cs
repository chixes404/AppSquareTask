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

namespace AppSquareTask.Core.MediatrHandelr.Trip.Queries.GetTripById
{
	public class GetTripByIdQueryHandler : IRequestHandler<GetTripByIdQuery, ApiResponse<TripDto>>
	{
		private readonly ITripService _tripService;
		private readonly IMapper _mapper;
		private readonly ApiResponseHandler _responseHandler;

		public GetTripByIdQueryHandler(ITripService tripService, IMapper mapper, ApiResponseHandler responseHandler)
		{
			_tripService = tripService;
			_mapper = mapper;
			_responseHandler = responseHandler;
		}

		public async Task<ApiResponse<TripDto>> Handle(GetTripByIdQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var trip = await _tripService.GetTripByIdAsync(request.Id);
				var tripDto = _mapper.Map<TripDto>(trip);
				return _responseHandler.Success(tripDto);
			}
			catch (KeyNotFoundException)
			{
				return _responseHandler.NotFound<TripDto>("Trip not found");
			}
		}
	}
}
