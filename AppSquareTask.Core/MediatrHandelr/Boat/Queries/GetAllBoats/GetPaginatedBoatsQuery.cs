using AppSquareTask.Core.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.MediatrHandelr.Boat.Queries.GetAllBoats
{
	public class GetPaginatedBoatsQuery : IRequest<PagedList<BoatDto>>
	{
		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 10;
	}
}
