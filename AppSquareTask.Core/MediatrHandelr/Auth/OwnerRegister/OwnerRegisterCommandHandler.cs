using MediatR;
using AppSquareTask.Core.Responses;
using AppSquareTask.Application.IServices;
using System.Threading;
using System.Threading.Tasks;
using AppSquareTask.Application.Services;
using Microsoft.Extensions.Logging;

namespace AppSquareTask.Core.MediatrHandelr.Auth.OwnerRegister
{
    public class OwnerRegisterCommandHandler : IRequestHandler<OwnerRegisterCommand, ApiResponse<AuthResultDto>>
	{
		private readonly IAuthService _authService;
		private readonly ApiResponseHandler _responseHandler;
		private readonly INotificationService _notificationService;
		private readonly ILogger<OwnerRegisterCommandHandler> _logger;
		public OwnerRegisterCommandHandler(IAuthService authService, ApiResponseHandler responseHandler, INotificationService notificationService, ILogger<OwnerRegisterCommandHandler> logger)
		{
			_authService = authService;
			_responseHandler = responseHandler;
			_notificationService = notificationService;
			_logger = logger;
		}

		public async Task<ApiResponse<AuthResultDto>> Handle(OwnerRegisterCommand request, CancellationToken cancellationToken)
		{
			var authResponse = await _authService.OwnerRegisterAsync(request.UserName , request.Email , request.Password);

			if (authResponse.Succeeded)
			{
				await _notificationService.NotifyAdminAsync($"A new owner has registered: {request.Email}");

				_logger.LogInformation("Notification sent for new owner registration: {Email}", request.Email);

				return _responseHandler.Success<AuthResultDto>(authResponse.Message);
			}
			_logger.LogWarning("Owner registration failed for {Email}: {Errors}", request.Email, string.Join(", ", authResponse.Errors));
			// Handle failure by returning errors
			return _responseHandler.BadRequest<AuthResultDto>(string.Join(", ", authResponse.Errors));
		}
	}
}
