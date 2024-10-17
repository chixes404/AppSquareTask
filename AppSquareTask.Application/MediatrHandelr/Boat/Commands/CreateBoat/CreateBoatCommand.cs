using AppSquareTask.Application.Responses;
using AppSquareTask.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.MediatrHandelr.Boat.Commands.CreateBoat
{
	public class CreateBoatCommand : IRequest<ApiResponse<BoatDto>>
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal PricePerPerson { get; set; }
		public int Capacity { get; set; }
		public DateTime MaxCancellationPeriod { get; set; }
		public Status Status { get; set; } = Status.Pending;
		public int OwnerId { get; set; }
	}
}
