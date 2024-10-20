using AppSquareTask.Core.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.MediatrHandelr.Trip.Queries.GetTripById
{
	public class GetTripByIdQuery : IRequest<ApiResponse<TripDto>>
	{
		public int Id { get; set; }
	}
}
