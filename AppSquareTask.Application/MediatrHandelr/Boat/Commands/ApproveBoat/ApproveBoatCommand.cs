using AppSquareTask.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.MediatrHandelr.Boat.Commands.ApproveBoat
{
	public class ApproveBoatCommand : IRequest<ApiResponse<string>>
	{
		public int BoatId { get; set; }
	}
}
