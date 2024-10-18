using AppSquareTask.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.MediatrHandelr.Booking.Commands.CancelBookingBoat
{
	public class CancelBookingBoatCommand : IRequest<bool>
	{
		public int BookingId { get; set; }
		public int CustomerId { get; set; }

		public CancelBookingBoatCommand(int bookingId, int customerId)
		{
			BookingId = bookingId;
			CustomerId = customerId;
		}

	}

}
