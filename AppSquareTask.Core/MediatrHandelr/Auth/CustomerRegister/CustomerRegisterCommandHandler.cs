using MediatR;
using AppSquareTask.Core.Responses;
using AppSquareTask.Application.IServices;
using System.Threading;
using System.Threading.Tasks;

namespace AppSquareTask.Core.MediatrHandelr.Auth.CustomerRegister
{
    public class CustomerRegisterCommandHandler : IRequestHandler<CustomerRegisterCommand, ApiResponse<AuthResultDto>>
	{
		private readonly IAuthService _authService;
		private readonly ApiResponseHandler _responseHandler;
		public CustomerRegisterCommandHandler(IAuthService authService, ApiResponseHandler responseHandler)
		{
			_authService = authService;
			_responseHandler = responseHandler;
		}

		public async Task<ApiResponse<AuthResultDto>> Handle(CustomerRegisterCommand request, CancellationToken cancellationToken)
		{
			var authResponse = await _authService.CustomerRegisterAsync(request.UserName,request.Email,request.Password);

			if (authResponse.Succeeded)
			{
				var authResultDto = new AuthResultDto
				{
					Token = authResponse.Token,
					TokenExpiration = authResponse.TokenExpiration
				};


				return _responseHandler.Success(authResultDto, authResponse.Message);
			}

			return _responseHandler.BadRequest<AuthResultDto>(string.Join(", ", authResponse.Errors));
		}
	}
}
