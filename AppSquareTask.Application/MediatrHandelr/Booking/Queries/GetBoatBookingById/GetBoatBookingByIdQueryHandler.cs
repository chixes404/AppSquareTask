using AppSquareTask.Application.IServices;
using AppSquareTask.Application.MediatrHandelr.Booking.Queries;
using AutoMapper;
using MediatR;

public class GetBoatBookingByIdQueryHandler : IRequestHandler<GetBoatBookingByIdQuery, BoatBookingDto>
{

	private readonly IBookingService _bookingService;
	private readonly IMapper _mapper;

	public GetBoatBookingByIdQueryHandler(IBookingService bookingService , IMapper mapper)
	{
		_bookingService = bookingService;
		_mapper = mapper;
	}

	public async Task<BoatBookingDto> Handle(GetBoatBookingByIdQuery request, CancellationToken cancellationToken)
	{
		// Fetch the booking from the repository
		var boatBooking = await _bookingService.GetBoatBookingByIdAsync(request.BookingId);

		return _mapper.Map<BoatBookingDto>(boatBooking);
	}
}
