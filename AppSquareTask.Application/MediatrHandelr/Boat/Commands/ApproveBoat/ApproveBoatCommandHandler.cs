using AppSquareTask.Application.IServices;
using AppSquareTask.Application.MediatrHandelr.Boat.Commands.ApproveBoat;
using AppSquareTask.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.MediatrHandelr.Boat.Commands.ApproveBoat
{
	public class ApproveBoatCommandHandler : IRequestHandler<ApproveBoatCommand, ApiResponse<string>>
	{
		private readonly IBoatService _boatService;
		private readonly ApiResponseHandler _responseHandler;

		public ApproveBoatCommandHandler(IBoatService boatService, ApiResponseHandler responseHandler)
		{
			_boatService = boatService;
			_responseHandler = responseHandler;
		}

		public async Task<ApiResponse<string>> Handle(ApproveBoatCommand request, CancellationToken cancellationToken)
		{
			var result = await _boatService.ApproveBoatAsync(request.BoatId);

			if (result)
			{
				return _responseHandler.Success<string>(null, "Boat approved successfully.");
			}

			return _responseHandler.BadRequest<string>("Failed to approve boat. Boat may not exist.");
		}
	}
}
