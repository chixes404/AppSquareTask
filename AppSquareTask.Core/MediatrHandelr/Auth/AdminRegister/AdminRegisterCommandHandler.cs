using MediatR;
using AppSquareTask.Core.Responses;
using AppSquareTask.Application.IServices;
using System.Threading;
using System.Threading.Tasks;
using AppSquareTask.Application.Services;
using Microsoft.Extensions.Logging;

namespace AppSquareTask.Core.MediatrHandelr.Auth.AdminRegister
{
	public class AdminRegisterCommandHandler : IRequestHandler<AdminRegisterCommand, ApiResponse<AuthResultDto>>
	{
		private readonly IAuthService _authService;
		private readonly ApiResponseHandler _responseHandler;
		private readonly INotificationService _notificationService;
		private readonly ILogger<AdminRegisterCommand> _logger;
		public AdminRegisterCommandHandler(IAuthService authService, ApiResponseHandler responseHandler, INotificationService notificationService, ILogger<AdminRegisterCommand> logger)
		{
			_authService = authService;
			_responseHandler = responseHandler;
			_notificationService = notificationService;
			_logger = logger;
		}

		public async Task<ApiResponse<AuthResultDto>> Handle(AdminRegisterCommand request, CancellationToken cancellationToken)
		{
			var authResponse = await _authService.AdminRegister(request.UserName, request.Email, request.Password);

			if (authResponse.Succeeded)
			{
				

				_logger.LogInformation("New Admin registered", request.Email);

				return _responseHandler.Success<AuthResultDto>(authResponse.Message);
			}
			_logger.LogWarning("Owner registration failed for {Email}: {Errors}", request.Email, string.Join(", ", authResponse.Errors));
			return _responseHandler.BadRequest<AuthResultDto>(string.Join(", ", authResponse.Errors));
		}
	}

}
