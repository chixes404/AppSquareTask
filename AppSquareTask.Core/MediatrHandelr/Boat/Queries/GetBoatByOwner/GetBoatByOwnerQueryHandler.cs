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

namespace AppSquareTask.Core.MediatrHandelr.Boat.Queries.GetBoatByOwner
{
	public class GetBoatByOwnerQueryHandler : IRequestHandler<GetBoatByOwnerQuery, ApiResponse<IEnumerable<BoatDto>>>
	{
		private readonly IBoatService _boatService;
		private readonly IMapper _mapper;
		private readonly ApiResponseHandler _responseHandler;

		public GetBoatByOwnerQueryHandler(IBoatService boatService, IMapper mapper, ApiResponseHandler responseHandler)
		{
			_boatService = boatService;
			_mapper = mapper;
			_responseHandler = responseHandler;
		}
		public async Task<ApiResponse<IEnumerable<BoatDto>>> Handle(GetBoatByOwnerQuery request, CancellationToken cancellationToken)
		{
			var boats = await _boatService.GetBoatsByOwnerAsync(request.OwnerId);
			var boatDtos = _mapper.Map<IEnumerable<BoatDto>>(boats);
			return _responseHandler.Success(boatDtos);
		}
	}
}
