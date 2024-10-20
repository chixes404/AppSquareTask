using AppSquareTask.Application.IServices;
using AppSquareTask.Core.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AppSquareTask.Core.MediatrHandelr.Auth.AdminLogin
{

		public class AdminLoginCommandHandler : IRequestHandler<AdminLoginCommand, ApiResponse<AuthResultDto>>
	{
		private readonly IAuthService _authService;
		private readonly ApiResponseHandler _responseHandler;
		public AdminLoginCommandHandler(IAuthService authService, ApiResponseHandler responseHandler)
		{
			_authService = authService;
			_responseHandler = responseHandler;
		}

		public async Task<ApiResponse<AuthResultDto>> Handle(AdminLoginCommand request, CancellationToken cancellationToken)
		{
			var authResponse = await _authService.AdminLoginAsync(request.Email, request.Password);

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
