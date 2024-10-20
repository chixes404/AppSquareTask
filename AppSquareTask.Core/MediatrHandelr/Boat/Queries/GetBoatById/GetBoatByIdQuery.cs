using AppSquareTask.Core.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.MediatrHandelr.Boat.Queries.GetBoatById
{
	public class GetBoatByIdQuery : IRequest<ApiResponse<BoatDto>>
	{
		public int Id { get; set; }
	}
}
