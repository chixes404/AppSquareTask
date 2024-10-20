using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.MediatrHandelr.Booking.Queries
{
	public class BoatBookingDto
	{
		public int BookingId { get; set; }
		public string BoatName { get; set; }
		public DateTime BookingDate { get; set; }
		public string Purpose { get; set; }
		public decimal TotalPrice { get; set; }
	}

}
