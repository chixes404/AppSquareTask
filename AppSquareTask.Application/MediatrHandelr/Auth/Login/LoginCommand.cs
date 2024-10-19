using AppSquareTask.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.MediatrHandelr.Auth.Login
{

    public class LoginCommand : IRequest<ApiResponse<AuthResultDto>>
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
