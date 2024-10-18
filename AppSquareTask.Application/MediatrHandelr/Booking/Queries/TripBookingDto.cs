using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.MediatrHandelr.Booking.Queries
{
	public class TripBookingDto
	{

		public int BookingId { get; set; }
		public string TripName { get; set; }
		public DateTime BookingDate { get; set; }
		public int Participants { get; set; }
		public decimal TotalPrice { get; set; }
	}
}
