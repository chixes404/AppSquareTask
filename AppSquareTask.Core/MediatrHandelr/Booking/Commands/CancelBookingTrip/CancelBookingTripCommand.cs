using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.MediatrHandelr.Booking.Commands.CancelBookingTrip
{
	public class CancelBookingTripCommand : IRequest<bool>
	{
		public int BookingId { get; set; }
		public int CustomerId { get; set; }

		public CancelBookingTripCommand(int bookingId, int customerId)
		{
			BookingId = bookingId;
			CustomerId = customerId;
	    	}
     	}
	}

