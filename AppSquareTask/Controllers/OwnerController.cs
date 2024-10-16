﻿using AppSquareTask.Application.MediatrHandelr.AdminManagment.ApproveOwner;
using AppSquareTask.Application.MediatrHandelr.AdminManagment.RejectOwner;
using AppSquareTask.Controllers.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppSquareTask.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OwnerController : AppControllerBase
	{





		[HttpPost("approve-owner/{ownerId}")]
		public async Task<IActionResult> ApproveOwner(int ownerId)
		{
			var result = await Mediator.Send(new ApproveOwnerCommand { OwnerId = ownerId });
			return result.Succeeded ? Ok(result) : BadRequest(result);
		}

		[HttpPost("rejext-owner/{ownerId}")]
		public async Task<IActionResult> RejectOwner(int ownerId)
		{
			var result = await Mediator.Send(new RejectOwnerCommand { OwnerId = ownerId });
			return result.Succeeded ? Ok(result) : BadRequest(result);
		}

	}
}
