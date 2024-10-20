﻿using AppSquareTask.Core.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.MediatrHandelr.Auth.AdminLogin
{
	public class AdminLoginCommand : IRequest<ApiResponse<AuthResultDto>>
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}