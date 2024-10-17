using AppSquareTask.Core.Models;
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

		Task<BoatBooking> BookBoatAsync(int boatId, Guid userId, int capacaity);

	}
}
