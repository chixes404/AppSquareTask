using AppSquareTask.Application.MediatrHandelr.Booking.Queries;
using MediatR;

public class GetBoatBookingByIdQuery : IRequest<BoatBookingDto>
{
	public int BookingId { get; }

	public GetBoatBookingByIdQuery(int bookingId)
	{
		BookingId = bookingId;
	}
}
