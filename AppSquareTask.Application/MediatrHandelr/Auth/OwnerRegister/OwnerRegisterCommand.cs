using AppSquareTask.Application.Dtos;
using AppSquareTask.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.MediatrHandelr.Auth.Register
{
	public class OwnerRegisterCommand : IRequest<ApiResponse<AuthResultDto>>
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
