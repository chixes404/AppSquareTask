using AppSquareTask.Application.Dtos;
using AppSquareTask.Application.IServices;
using AppSquareTask.Application.Responses;
using AppSquareTask.Core.IRepositories;
using AppSquareTask.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppSquareTask.Application.Services
{
	public class BoatService : IBoatService
	{
		private readonly IUnitOfWork _unitOfWork;

		public BoatService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Boat> CreateBoatAsync(Boat boat)
		{
			// Set the boat status to Pending by default
			boat.Status = Status.Pending;

			await _unitOfWork.BoatRepository.CreateAsync(boat);
			await _unitOfWork.SaveAsync();



			return boat;
		}

		public async Task<Boat> UpdateBoatAsync(Boat boat)
		{
			// Ensure the boat exists before updating
			var existingBoat = await _unitOfWork.BoatRepository.GetById(boat.Id);
			if (existingBoat == null) throw new KeyNotFoundException("Boat not found.");

			// Update properties as needed
			existingBoat.Name = boat.Name;
			existingBoat.Description = boat.Description;
			existingBoat.PricePerPerson = boat.PricePerPerson;
			existingBoat.Capacity = boat.Capacity;
			existingBoat.Status = boat.Status;

			await _unitOfWork.BoatRepository.UpdateAsync(existingBoat);
			await _unitOfWork.SaveAsync();

			return existingBoat; // Return the updated boat
		}

		public async Task DeleteBoatAsync(int boatId)
		{
			var boat = await _unitOfWork.BoatRepository.GetById(boatId);
			if (boat == null) throw new KeyNotFoundException("Boat not found.");
			_unitOfWork.BoatRepository.DeleteAsync(boat);
			await _unitOfWork.SaveAsync();
		}

		public async Task<Boat> GetBoatByIdAsync(int boatId)
		{
			var boats = await _unitOfWork.Repository<Boat>()
						  .FindAsync(s => s.Id == boatId, include: q => q.Include(s => s.Owner));
			var boat = boats.FirstOrDefault(); 
			
			if (boat == null || boat.Status != Status.Approved)
			{
				throw new KeyNotFoundException("Boat not found or not approved.");
			}
			return boat;
		}



		public async Task<IEnumerable<ResponseBoatDto>> GetBoatsByOwnerAsync(int ownerId)
		{
			// Fetch boats by OwnerId
			var boats = await _unitOfWork.Repository<Boat>()
				.FindAsync(s => true, include: q => q.Include(s => s.Owner));
			return (IEnumerable<ResponseBoatDto>)boats.Where(boat => boat.OwnerId == ownerId && boat.Status == Status.Approved);
		}

		public async Task<PagedList<ResponseBoatDto>> GetAllBoatsPaginatedAsync(int pageNumber, int pageSize)
		{
			var allBoats = await _unitOfWork.BoatRepository.GetAllAsync();
			var approvedBoats = allBoats.Where(boat => boat.Status == Status.Approved);

			var totalCount = approvedBoats.Count(); // Count approved boats
			var boatsToReturn = approvedBoats.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(); // Get the requested page

			return new PagedList<ResponseBoatDto>((IEnumerable<ResponseBoatDto>)boatsToReturn, pageNumber, pageSize, totalCount); // Create PagedList
		}
	}
}
