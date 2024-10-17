using AppSquareTask.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.MediatrHandelr.Trip.Queries.GetTripByOwner
{
	public class GetTripByOwnerQuery : IRequest<ApiResponse<IEnumerable<TripDto>>>
	{
		public int OwnerId { get; set; }
	}
}
