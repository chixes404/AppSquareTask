﻿using AppSquareTask.Application.MediatrHandelr.AdminManagment.ApproveOwner;
using AppSquareTask.Application.MediatrHandelr.AdminManagment.RejectOwner;
using AppSquareTask.Application.MediatrHandelr.Auth.CustomerRegister;
using AppSquareTask.Application.MediatrHandelr.Auth.Login;
using AppSquareTask.Application.MediatrHandelr.Auth.Register;
using AppSquareTask.Controllers.Base;
using AppSquareTask.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppSquareTask.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : AppControllerBase
	{

		[HttpPost("register-owner")]
		public async Task<IActionResult> RegisterOwner([FromBody] OwnerRegisterCommand command)
		{
			var result = await Mediator.Send(command);
			return CreateResponse(result);
		}


		[HttpPost("register-customer")]
		public async Task<IActionResult> RegisterCustomer([FromBody] CustomerRegisterCommand command)
		{
			var result = await Mediator.Send(command);
			return CreateResponse(result);
		}


		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginCommand command)
		{
			var result = await Mediator.Send(command);
			return CreateResponse(result);
		}


		

	}
}
