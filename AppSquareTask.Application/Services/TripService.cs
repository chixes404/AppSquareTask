using AppSquareTask.Application.IServices;
using AppSquareTask.Application.Responses;
using AppSquareTask.Core.IRepositories;
using AppSquareTask.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppSquareTask.Application.Services
{
	public class TripService : ITripService
	{
		private readonly IUnitOfWork _unitOfWork;

		public TripService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Trip> CreateTripAsync(Trip trip)
		{
			// Set the trip status to Pending by default
			trip.Status = Status.Pending;

			await _unitOfWork.TripRepository.CreateAsync(trip);
			await _unitOfWork.SaveAsync();



			return trip;
		}

		public async Task<Trip> UpdateTripAsync(Trip trip)
		{
			// Ensure the trip exists before updating
			var existingTrip = await _unitOfWork.TripRepository.GetById(trip.Id);
			if (existingTrip == null) throw new KeyNotFoundException("Trip not found.");

			// Update properties as needed
			existingTrip.Name = trip.Name;
			existingTrip.Description = trip.Description;
			existingTrip.PricePerPerson = trip.PricePerPerson;
			existingTrip.Capacity = trip.Capacity;
			existingTrip.Status = trip.Status;

			await _unitOfWork.TripRepository.UpdateAsync(existingTrip);
			await _unitOfWork.SaveAsync();

			return existingTrip; // Return the updated trip
		}

		public async Task DeleteTripAsync(int tripId)
		{
			var trip = await _unitOfWork.TripRepository.GetById(tripId);
			if (trip == null) throw new KeyNotFoundException("Trip not found.");
			_unitOfWork.TripRepository.DeleteAsync(trip);
			await _unitOfWork.SaveAsync();
		}

		public async Task<Trip> GetTripByIdAsync(int tripId)
		{
			var trip = await _unitOfWork.TripRepository.GetById(tripId);
			if (trip == null || trip.Status != Status.Approved)
			{
				throw new KeyNotFoundException("Trip not found or not approved.");
			}
			return trip;
		}



		public async Task<IEnumerable<Trip>> GetTripsByOwnerAsync(int ownerId)
		{
			// Fetch trips by OwnerId
			var trips = await _unitOfWork.TripRepository.GetAllAsync();
			return trips.Where(trip => trip.OwnerId == ownerId && trip.Status == Status.Approved);
		}

		public async Task<PagedList<Trip>> GetAllTripsPaginatedAsync(int pageNumber, int pageSize)
		{
			var allTrips = await _unitOfWork.TripRepository.GetAllAsync();
			var approvedTrips = allTrips.Where(trip => trip.Status == Status.Approved);

			var totalCount = approvedTrips.Count(); // Count approved trips
			var tripsToReturn = approvedTrips.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(); // Get the requested page

			return new PagedList<Trip>(tripsToReturn, pageNumber, pageSize, totalCount); // Create PagedList
		}
	}
}
