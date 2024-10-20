using AppSquareTask.Core.MediatrHandelr.AdminManagment.ApproveOwner;
using AppSquareTask.Core.MediatrHandelr.AdminManagment.RejectOwner;
using AppSquareTask.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppSquareTask.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OwnerController : AppControllerBase
	{




		[Authorize(Roles = "Admin")]

		[HttpPost("approve-owner/{ownerId}")]
		public async Task<IActionResult> ApproveOwner(int ownerId)
		{
			var result = await Mediator.Send(new ApproveOwnerCommand { OwnerId = ownerId });
			return result.Succeeded ? Ok(result) : BadRequest(result);
		}
		[Authorize(Roles = "Admin")]

		[HttpPost("rejext-owner/{ownerId}")]
		public async Task<IActionResult> RejectOwner(int ownerId)
		{
			var result = await Mediator.Send(new RejectOwnerCommand { OwnerId = ownerId });
			return result.Succeeded ? Ok(result) : BadRequest(result);
		}

	}
}
