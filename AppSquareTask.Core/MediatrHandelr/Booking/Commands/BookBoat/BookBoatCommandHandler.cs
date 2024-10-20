using AppSquareTask.Application.IServices;
using AppSquareTask.Core.MediatrHandelr.Auth.OwnerRegister;
using AppSquareTask.Core.MediatrHandelr.Booking.Commands.BookBoat;
using AppSquareTask.Data.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

public class BookBoatCommandHandler : IRequestHandler<BookBoatCommand, BoatBooking>
{
	private readonly IBookingService _bookingService;
	private readonly ILogger<BookBoatCommandHandler> _logger;

	public BookBoatCommandHandler(IBookingService bookingService, ILogger<BookBoatCommandHandler> logger)
	{
		_bookingService = bookingService;
		_logger = logger;
	}

	public async Task<BoatBooking> Handle(BookBoatCommand request, CancellationToken cancellationToken)
	{
		_logger.LogInformation($"there is a new booking for boat with id {request.BoatId} through user" , request.UserId);

		return await _bookingService.BookBoatAsync(request.BoatId, request.UserId, request.Purpose , request.BookingDate);

	}
}
