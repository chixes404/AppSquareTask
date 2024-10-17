using AppSquareTask.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.MediatrHandelr.Boat.Queries.GetBoatByOwner
{
	public class GetBoatByOwnerQuery : IRequest<ApiResponse<IEnumerable<BoatDto>>>
	{
		public int OwnerId { get; set; }
	}
}
