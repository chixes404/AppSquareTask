using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AppSquareTask.Application.IServices;
using AppSquareTask.Core.Responses;
using AppSquareTask.Core.MediatrHandelr.AdminManagment.ApproveOwner;

public class ApproveOwnerCommandHandler : IRequestHandler<ApproveOwnerCommand, ApiResponse<string>>
{
	private readonly IOwnerService _ownerService;
	private readonly ApiResponseHandler _responseHandler;

	public ApproveOwnerCommandHandler(IOwnerService ownerService, ApiResponseHandler responseHandler)
	{
		_ownerService = ownerService;
		_responseHandler = responseHandler;
	}

	public async Task<ApiResponse<string>> Handle(ApproveOwnerCommand request, CancellationToken cancellationToken)
	{
		var result = await _ownerService.ApproveOwnerAsync(request.OwnerId);

		if (result)
		{
			return _responseHandler.Success<string>(null, "Owner approved successfully.");
		}

		return _responseHandler.BadRequest<string>("Failed to approve owner. Owner may not exist.");
	}
}
