using AppSquareTask.Application.IServices;
using AppSquareTask.Core.MediatrHandelr.Booking.Commands.BookBoat;
using AppSquareTask.Data.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class BookBoatCommandHandler : IRequestHandler<BookBoatCommand, BoatBooking>
{
	private readonly IBookingService _bookingService;

	public BookBoatCommandHandler(IBookingService bookingService)
	{
		_bookingService = bookingService;
	}

	public async Task<BoatBooking> Handle(BookBoatCommand request, CancellationToken cancellationToken)
	{
		return await _bookingService.BookBoatAsync(request.BoatId, request.UserId, request.Purpose , request.BookingDate);
	}
}
