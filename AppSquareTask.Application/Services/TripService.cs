using AppSquareTask.Application.Dtos;
using AppSquareTask.Application.IServices;
using AppSquareTask.Infrastracture.IRepositories;
using AppSquareTask.Data.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppSquareTask.Application.Services
{
	public class TripService : ITripService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public TripService(IUnitOfWork unitOfWork , IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Trip> CreateTripAsync(Trip trip)
		{
			trip.Status = Status.Pending;

			await _unitOfWork.TripRepository.CreateAsync(trip);
			await _unitOfWork.SaveAsync();



			return trip;
		}

		public async Task<Trip> UpdateTripAsync(Trip trip)
		{
			var existingTrip = await _unitOfWork.TripRepository.GetById(trip.Id);
			if (existingTrip == null) throw new KeyNotFoundException("Trip not found.");

			existingTrip.Name = trip.Name;
			existingTrip.Description = trip.Description;
			existingTrip.PricePerPerson = trip.PricePerPerson;
			existingTrip.Capacity = trip.Capacity;
			existingTrip.Status = trip.Status;

			await _unitOfWork.TripRepository.UpdateAsync(existingTrip);
			await _unitOfWork.SaveAsync();

			return existingTrip; 
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
			var trips = await _unitOfWork.Repository<Trip>()
									  .FindAsync(s => s.Id == tripId, include: q => q.Include(s => s.Owner));
			var trip = trips.FirstOrDefault(); 
			if (trip == null || trip.Status != Status.Approved)
			{
				throw new KeyNotFoundException("Trip not found or not approved.");
			}
			return trip;
		}



		public async Task<IEnumerable<ResponseTripDto>> GetTripsByOwnerAsync(int ownerId)
		{
			var trips = await _unitOfWork.Repository<Trip>()
				.FindAsync(trip => trip.OwnerId == ownerId && trip.Status == Status.Approved,
						   include: q => q.Include(trip => trip.Owner));

			var mappedTrips = trips.Select(trip => _mapper.Map<ResponseTripDto>(trip));

			return mappedTrips;
		}


	
	}
}
