using AppSquareTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.IServices
{
	public interface IBookingService
	{
		Task<TripBooking> BookTripAsync(int tripId, Guid userId, int numberOfParticipants);

		Task<BoatBooking> BookBoatAsync(int boatId, Guid userId, string purpose ,DateTime date);

		Task CancelTripBookingAsync(int bookingId, int customerId);
		Task CancelBoatBookingAsync(int bookingId, int customerId);

		Task<BoatBooking> GetBoatBookingByIdAsync(int bookingId);
		Task<TripBooking> GetTripBookingByIdAsync(int bookingId);
	}
}
