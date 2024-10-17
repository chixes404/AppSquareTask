using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSquareTask.Core.Models.Common;

namespace AppSquareTask.Core.Models
{
    public class Boat : BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal PricePerPerson { get; set; }
		public int Capacity { get; set; }
		public DateTime MaxCancellationPeriod { get; set; }
		public Status Status { get; set; } = Status.Pending; // "Pending", "Approved", "Rejected"
		public int OwnerId { get; set; }
		public Owner Owner { get; set; } // Owner of the boat (a User with Role "Owner")
		public ICollection<Trip> Trips { get; set; }
		public ICollection<BoatBooking> BoatBookings { get; set; } // New: Bookings for the boat itself
	}



}

