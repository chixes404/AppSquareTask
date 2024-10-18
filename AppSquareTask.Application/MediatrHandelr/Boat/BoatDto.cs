using AppSquareTask.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.MediatrHandelr.Boat
{
	public class BoatDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public int Capacity { get; set; }
		public Status Status { get; set; } = Status.Pending;
		public string ? OwnerName { get; set; }
		public int OwnerId { get; set; }


	}
}
