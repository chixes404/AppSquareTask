using AppSquareTask.Application.Dtos;
using AppSquareTask.Application.IServices;
using AppSquareTask.Infrastracture.IRepositories;
using AppSquareTask.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
		private readonly IMapper _mapper;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IEmailService _emailService;


		public BoatService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager , IEmailService emailService)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_userManager = userManager;
			_emailService = emailService;
		}

		public async Task<Boat> CreateBoatAsync(Boat boat)
		{
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
			existingBoat.Price = boat.Price;
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
			var boats = await _unitOfWork.Repository<Boat>()
				.FindAsync(boat => boat.OwnerId == ownerId && boat.Status == Status.Approved,
						   include: q => q.Include(boat => boat.Owner));

			var mappedBoats = boats.Select(boat => _mapper.Map<ResponseBoatDto>(boat));

			return mappedBoats;
		}


		


		public async Task<bool> ApproveBoatAsync(int boatId)
		{
			var boat = await _unitOfWork.BoatRepository.GetById(boatId);
			if (boat == null) return false; 

			boat.Status = Status.Approved;

			await _unitOfWork.SaveAsync();

			var owner = await _unitOfWork.OwnerRepository.GetById(boat.OwnerId);
			if (owner == null) return false; 

			var user = await _userManager.FindByIdAsync(owner.UserId.ToString());
			if (user == null) return false; 

			await _emailService.SendEmailAsync(user.Email!, "Admin Approved Your Boat",
			   "Your boat has been approved and is now available for booking.");

			return true; 
		}



		public async Task<bool> RejectBoatAsync(int boatId)
		{
			var boat = await _unitOfWork.BoatRepository.GetById(boatId);
			if (boat == null) return false; 

			boat.Status = Status.Rejected;
			await _unitOfWork.SaveAsync();

			var owner = await _unitOfWork.OwnerRepository.GetById(boat.OwnerId);
			if (owner == null) return false;

			var user = await _userManager.FindByIdAsync(owner.UserId.ToString());
			if (user == null) return false; 

			await _emailService.SendEmailAsync(user.Email!, "Admin Rejected Your Boat",
			   "Your boat has been rejected. Please call customer support for further assistance.");

			return true; 
		}












	}
}
