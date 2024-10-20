using AppSquareTask.Application.IServices;
using AppSquareTask.Core.MediatrHandelr.Booking.Queries;
using AutoMapper;
using MediatR;

public class GetTripBookingByIdQueryHandler : IRequestHandler<GetTripBookingByIdQuery, TripBookingDto>
{

	private readonly IBookingService _bookingService;
	private readonly IMapper _mapper;

	public GetTripBookingByIdQueryHandler(IBookingService bookingService, IMapper mapper)
	{
		_bookingService = bookingService;
		_mapper = mapper;
	}

	public async Task<TripBookingDto> Handle(GetTripBookingByIdQuery request, CancellationToken cancellationToken)
	{
		// Fetch the booking from the repository
		var tripBooking = await _bookingService.GetTripBookingByIdAsync(request.BookingId);

		return _mapper.Map<TripBookingDto>(tripBooking);
	}
}
