using AppSquareTask.Core.Responses;
using AppSquareTask.Core.MediatrHandelr.Auth.CustomerRegister;
using AppSquareTask.Core.MediatrHandelr.Auth.Login;
using AppSquareTask.Controllers.Base;
using AppSquareTask.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AppSquareTask.Core.MediatrHandelr.Auth.OwnerRegister;

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
