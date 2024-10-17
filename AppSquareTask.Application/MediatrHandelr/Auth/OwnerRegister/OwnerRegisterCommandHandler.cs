using MediatR;
using AppSquareTask.Application.Responses;
using AppSquareTask.Application.IServices;
using System.Threading;
using System.Threading.Tasks;
using AppSquareTask.Application.Dtos;
using AppSquareTask.Application.Services;

namespace AppSquareTask.Application.MediatrHandelr.Auth.Register
{
	public class OwnerRegisterCommandHandler : IRequestHandler<OwnerRegisterCommand, ApiResponse<AuthResultDto>>
	{
		private readonly IAuthService _authService;
		private readonly ApiResponseHandler _responseHandler;
		private readonly INotificationService _notificationService;
		public OwnerRegisterCommandHandler(IAuthService authService, ApiResponseHandler responseHandler, INotificationService notificationService)
		{
			_authService = authService;
			_responseHandler = responseHandler;
			_notificationService = notificationService;
		}

		public async Task<ApiResponse<AuthResultDto>> Handle(OwnerRegisterCommand request, CancellationToken cancellationToken)
		{
			// Call the OwnerRegisterAsync method from the AuthService
			var authResponse = await _authService.OwnerRegisterAsync(request);

			if (authResponse.Succeeded)
			{
				await _notificationService.NotifyAdminAsync($"A new owner has registered: {request.Email}");
				var authResultDto = new AuthResultDto
				{
					Token = null,
					TokenExpiration = DateTime.MinValue 
				};

				return _responseHandler.Success(authResultDto, authResponse.Message);
			}

			// Handle failure by returning errors
			return _responseHandler.BadRequest<AuthResultDto>(string.Join(", ", authResponse.Errors));
		}
	}
}
