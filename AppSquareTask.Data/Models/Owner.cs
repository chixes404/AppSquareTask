using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSquareTask.Data.Models.Common;

namespace AppSquareTask.Data.Models
{
    public class Owner  : BaseEntity
	{
		public Guid UserId { get; set; }


		public ApplicationUser User { get; set; }
		public ICollection<Boat> Boats { get; set; } = new List<Boat>(); // Owned boats
		public ICollection<Trip> Trips { get; set; } = new List<Trip>(); // Owned boats



	}
}
