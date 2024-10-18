using AppSquareTask.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.MediatrHandelr.Booking.Commands.BookBoat
{
	public class BookBoatCommand : IRequest<BoatBooking>
	{
		public int BoatId { get; set; }
		public Guid UserId { get; set; }
		public string Purpose { get; set; }
		public DateTime BookingDate { get; set; }
	}
}
