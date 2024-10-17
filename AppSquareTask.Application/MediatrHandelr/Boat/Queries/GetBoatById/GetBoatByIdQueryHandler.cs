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

namespace AppSquareTask.Application.MediatrHandelr.Boat.Queries.GetBoatById
{
	public class GetBoatByIdQueryHandler : IRequestHandler<GetBoatByIdQuery, ApiResponse<BoatDto>>
	{
		private readonly IBoatService _boatService;
		private readonly IMapper _mapper;
		private readonly ApiResponseHandler _responseHandler;

		public GetBoatByIdQueryHandler(IBoatService boatService, IMapper mapper, ApiResponseHandler responseHandler)
		{
			_boatService = boatService;
			_mapper = mapper;
			_responseHandler = responseHandler;
		}

		public async Task<ApiResponse<BoatDto>> Handle(GetBoatByIdQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var boat = await _boatService.GetBoatByIdAsync(request.Id);
				var boatDto = _mapper.Map<BoatDto>(boat);
				return _responseHandler.Success(boatDto);
			}
			catch (KeyNotFoundException)
			{
				return _responseHandler.NotFound<BoatDto>("Boat not found");
			}
		}
	}
}
