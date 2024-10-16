using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.Models
{
	public class Customer : ApplicationUser
	{

		public Wallet Wallet { get; set; } // Associated wallet for payments
		public ICollection<BoatBooking> BoatBookings { get; set; } = new List<BoatBooking>(); // Bookings made by the Customer
		public ICollection<TripBooking> TripBookings { get; set; } = new List<TripBooking>();
	}
}
