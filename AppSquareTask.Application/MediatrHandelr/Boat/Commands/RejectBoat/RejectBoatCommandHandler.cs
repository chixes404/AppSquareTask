using AppSquareTask.Application.IServices;
using AppSquareTask.Application.MediatrHandelr.Boat.Commands.RejectBoat;
using AppSquareTask.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.MediatrHandelr.Boat.Commands.RejectBoat
{
	public class RejectBoatCommandHandler : IRequestHandler<RejectBoatCommand, ApiResponse<string>>
	{
		private readonly IBoatService _boatService;
		private readonly ApiResponseHandler _responseHandler;

		public RejectBoatCommandHandler(IBoatService boatService, ApiResponseHandler responseHandler)
		{
			_boatService = boatService;
			_responseHandler = responseHandler;
		}

		public async Task<ApiResponse<string>> Handle(RejectBoatCommand request, CancellationToken cancellationToken)
		{
			var result = await _boatService.RejectBoatAsync(request.BoatId);

			if (result)
			{
				return _responseHandler.Success<string>(null, "Boat approved successfully.");
			}

			return _responseHandler.BadRequest<string>("Failed to approve boat. Boat may not exist.");
		}
	}

}
