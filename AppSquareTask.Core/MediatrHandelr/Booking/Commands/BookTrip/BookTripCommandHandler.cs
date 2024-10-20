using AppSquareTask.Application.IServices;
using AppSquareTask.Core.MediatrHandelr.Booking.Commands.BookTrip;
using AppSquareTask.Data.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class BookTripCommandHandler : IRequestHandler<BookTripCommand, TripBooking>
{
	private readonly IBookingService _bookingService;

	public BookTripCommandHandler(IBookingService bookingService)
	{
		_bookingService = bookingService;
	}

	public async Task<TripBooking> Handle(BookTripCommand request, CancellationToken cancellationToken)
	{
		return await _bookingService.BookTripAsync(request.TripId, request.UserId, request.NumberOfParticipants);
	}
}
