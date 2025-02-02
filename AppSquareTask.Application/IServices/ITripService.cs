﻿using AppSquareTask.Application.Dtos;
using AppSquareTask.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppSquareTask.Application.IServices
{
	public interface ITripService
	{
		Task<Trip> GetTripByIdAsync(int tripId);
		Task<IEnumerable<ResponseTripDto>> GetTripsByOwnerAsync(int ownerId); // New method
		Task<Trip> CreateTripAsync(Trip trip); // Return Trip after creation
		Task<Trip> UpdateTripAsync(Trip trip); // Return updated Trip
		Task DeleteTripAsync(int tripId);
	}
}
