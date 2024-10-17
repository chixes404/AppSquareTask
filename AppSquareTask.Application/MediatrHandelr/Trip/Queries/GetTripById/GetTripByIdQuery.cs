using AppSquareTask.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.MediatrHandelr.Trip.Queries.GetTripById
{
	public class GetTripByIdQuery : IRequest<ApiResponse<TripDto>>
	{
		public int Id { get; set; }
	}
}
