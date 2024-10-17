using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AppSquareTask.Application.IServices;
using AppSquareTask.Application.Responses;
using AppSquareTask.Application.MediatrHandelr.AdminManagment.ApproveOwner;

public class RejectOwnerCommandHandler : IRequestHandler<ApproveOwnerCommand, ApiResponse<string>>
{
	private readonly IOwnerService _ownerService;
	private readonly ApiResponseHandler _responseHandler;

	public RejectOwnerCommandHandler(IOwnerService ownerService, ApiResponseHandler responseHandler)
	{
		_ownerService = ownerService;
		_responseHandler = responseHandler;
	}

	public async Task<ApiResponse<string>> Handle(ApproveOwnerCommand request, CancellationToken cancellationToken)
	{
		var result = await _ownerService.RejectOwnerAsync(request.OwnerId);

		if (result)
		{
			return _responseHandler.Success<string>(null, "Owner Rejected successfully.");
		}

		return _responseHandler.BadRequest<string>("Failed to reject owner. Owner may not exist.");
	}
}
