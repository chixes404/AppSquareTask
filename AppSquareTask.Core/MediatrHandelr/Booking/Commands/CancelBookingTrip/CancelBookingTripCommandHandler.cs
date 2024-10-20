using System.Threading;
using System.Threading.Tasks;
using AppSquareTask.Application.IServices;
using AppSquareTask.Core.MediatrHandelr.Booking.Commands.CancelBookingTrip;
using AppSquareTask.Data.Models;
using MediatR;

namespace AppSquareTask.Core.MediatrHandelr.Booking.Commands.CancelBookingBoat
{
	public class CancelBookingTripCommandHandler : IRequestHandler<CancelBookingTripCommand, bool>
	{
		private readonly IBookingService _bookingService;

		public CancelBookingTripCommandHandler(IBookingService bookingService)
		{
			_bookingService = bookingService;
		}

		public async Task<bool> Handle(CancelBookingTripCommand request, CancellationToken cancellationToken)
		{
			await _bookingService.CancelTripBookingAsync(request.BookingId, request.CustomerId);
			return true;
		}
	}
}
