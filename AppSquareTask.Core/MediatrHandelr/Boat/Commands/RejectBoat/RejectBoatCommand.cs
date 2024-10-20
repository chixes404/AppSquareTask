using AppSquareTask.Core.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.MediatrHandelr.Boat.Commands.RejectBoat
{
	public class RejectBoatCommand : IRequest<ApiResponse<string>>
	{
		public int BoatId { get; set; }
	}
}
