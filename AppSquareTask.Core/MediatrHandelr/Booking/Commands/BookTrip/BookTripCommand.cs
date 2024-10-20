using AppSquareTask.Data.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.MediatrHandelr.Booking.Commands.BookTrip
{
	public class BookTripCommand : IRequest<TripBooking>
	{
		public int TripId { get; set; }
		public Guid UserId { get; set; }
		public int NumberOfParticipants { get; set; }
	}
}
