using System.Threading;
using System.Threading.Tasks;
using AppSquareTask.Application.IServices;
using AppSquareTask.Data.Models;
using MediatR;

namespace AppSquareTask.Core.MediatrHandelr.Booking.Commands.CancelBookingBoat
{
	public class CancelBoatBookingCommandHandler : IRequestHandler<CancelBookingBoatCommand, bool>
	{
		private readonly IBookingService _bookingService;

		public CancelBoatBookingCommandHandler(IBookingService bookingService)
		{
			_bookingService = bookingService;
		}

		public async Task<bool> Handle(CancelBookingBoatCommand request, CancellationToken cancellationToken)
		{
			await _bookingService.CancelBoatBookingAsync(request.BookingId, request.CustomerId);
			return true;
		}
	}
}
