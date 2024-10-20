using AppSquareTask.Core.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.MediatrHandelr.AdminManagment.RejectOwner
{
	public class RejectOwnerCommand : IRequest<ApiResponse<string>>
	{
		public int OwnerId { get; set; }
	}
}
