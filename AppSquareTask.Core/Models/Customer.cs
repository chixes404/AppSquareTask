using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSquareTask.Core.Models.Common;

namespace AppSquareTask.Core.Models
{
    public class Customer : BaseEntity
	{
		public Guid UserId { get; set; }


		public ApplicationUser User { get; set; }
		public ICollection<BoatBooking> BoatBookings { get; set; } = new List<BoatBooking>(); // Bookings made by the Customer
		public ICollection<TripBooking> TripBookings { get; set; } = new List<TripBooking>();


	}
}
