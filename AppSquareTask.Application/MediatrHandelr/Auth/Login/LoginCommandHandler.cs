using AppSquareTask.Application.IServices;
using AppSquareTask.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.MediatrHandelr.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<AuthResultDto>>
	{
		private readonly IAuthService _authService;
		private readonly ApiResponseHandler _responseHandler; 
		public LoginCommandHandler(IAuthService authService, ApiResponseHandler responseHandler)
		{
			_authService = authService;
			_responseHandler = responseHandler;
		}

		public async Task<ApiResponse<AuthResultDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			var authResponse = await _authService.LoginAsync(request);

			if (authResponse.Succeeded)
			{
				var authResultDto = new AuthResultDto
				{
					Token = authResponse.Token,
					TokenExpiration = authResponse.TokenExpiration
				};

				return _responseHandler.Success(authResultDto, authResponse.Message);
			}

			return _responseHandler.BadRequest<AuthResultDto>(authResponse.Message);
		}
	}
}
