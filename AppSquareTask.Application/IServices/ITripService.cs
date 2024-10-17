using AppSquareTask.Application.Responses;
using AppSquareTask.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppSquareTask.Application.IServices
{
	public interface ITripService
	{
		Task<Trip> GetTripByIdAsync(int tripId);
		Task<PagedList<Trip>> GetAllTripsPaginatedAsync(int pageNumber, int pageSize);
		Task<IEnumerable<Trip>> GetTripsByOwnerAsync(int ownerId); // New method
		Task<Trip> CreateTripAsync(Trip trip); // Return Trip after creation
		Task<Trip> UpdateTripAsync(Trip trip); // Return updated Trip
		Task DeleteTripAsync(int tripId);
	}
}
