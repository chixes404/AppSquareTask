using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.Models
{
	public class Trip : BaseEntity
	{

		public string Name { get; set; }
		public string Description { get; set; }
		public decimal PricePerPerson { get; set; }
		public int Capacity { get; set; }
		public DateTime MaxCancellationPeriod { get; set; }
		public Guid OwnerId { get; set; }
		public ApplicationUser Owner { get; set; } 

		public int BoatId { get; set; }
		public Boat Boat { get; set; }
		public ICollection<TripBooking> Bookings { get; set; }
	}



}
