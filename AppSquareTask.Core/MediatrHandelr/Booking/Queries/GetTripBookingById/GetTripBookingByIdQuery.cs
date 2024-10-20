using AppSquareTask.Core.MediatrHandelr.Booking.Queries;
using MediatR;

public class GetTripBookingByIdQuery : IRequest<TripBookingDto>
{
	public int BookingId { get; }

	public GetTripBookingByIdQuery(int bookingId)
	{
		BookingId = bookingId;
	}
}
