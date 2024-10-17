using AppSquareTask.Application.IServices;
using AppSquareTask.Application.Responses;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.MediatrHandelr.Trip.Queries.GetTripByOwner
{
	public class GetTripByOwnerQueryHandler : IRequestHandler<GetTripByOwnerQuery, ApiResponse<IEnumerable<TripDto>>>
	{
		private readonly ITripService _tripService;
		private readonly IMapper _mapper;
		private readonly ApiResponseHandler _responseHandler;

		public GetTripByOwnerQueryHandler(ITripService tripService, IMapper mapper, ApiResponseHandler responseHandler)
		{
			_tripService = tripService;
			_mapper = mapper;
			_responseHandler = responseHandler;
		}
		public async Task<ApiResponse<IEnumerable<TripDto>>> Handle(GetTripByOwnerQuery request, CancellationToken cancellationToken)
		{
			var trips = await _tripService.GetTripsByOwnerAsync(request.OwnerId);
			var tripDtos = _mapper.Map<IEnumerable<TripDto>>(trips);
			return _responseHandler.Success(tripDtos);
		}
	}
}
