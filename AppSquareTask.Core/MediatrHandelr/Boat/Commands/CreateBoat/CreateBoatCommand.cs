using AppSquareTask.Core.Responses;
using AppSquareTask.Data.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.MediatrHandelr.Boat.Commands.CreateBoat
{
	public class CreateBoatCommand : IRequest<ApiResponse<BoatDto>>
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public int Capacity { get; set; }
		public int OwnerId { get; set; }
	}
}
