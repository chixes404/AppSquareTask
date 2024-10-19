using AppSquareTask.Application.IServices;
using AppSquareTask.Core.IRepositories;
using AppSquareTask.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace AppSquareTask.Application.Services
{
	public class OwnerService : IOwnerService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IEmailService _emailService;

		public OwnerService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IEmailService emailService)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
			_emailService = emailService;
		}

		public async Task<Owner> GetOwnerByIdAsync(int ownerId)
		{
			return await _unitOfWork.OwnerRepository.GetById(ownerId);
		}

		public async Task<bool> ApproveOwnerAsync(int ownerId)
		{
			var owner = await _unitOfWork.OwnerRepository.GetById(ownerId);
			if (owner == null) return false;

			var user = await _userManager.FindByIdAsync(owner.UserId.ToString());
			if (user == null) return false;

			user.Status = Status.Approved;
			await _userManager.UpdateAsync(user);
			await _unitOfWork.SaveAsync();

			await _emailService.SendEmailAsync(user.Email!, "Admin Approved Your Registeration",
			   "you can login now");

			return true;
		}

		public async Task<bool> RejectOwnerAsync(int ownerId)
		{
			var owner = await _unitOfWork.OwnerRepository.GetById(ownerId);
			if (owner == null) return false;

			var user = await _userManager.FindByIdAsync(owner.UserId.ToString());
			if (user == null) return false;

			// Reject the owner by updating the user's status
			user.Status = Status.Rejected;
			await _userManager.UpdateAsync(user);
			await _unitOfWork.SaveAsync();


			await _emailService.SendEmailAsync(user.Email!, "Admin Rejected Your Registeration",
			   "please call the customer support");

			return true;
		}



		public async Task CreateOwnerAsync(Owner owner, CancellationToken cancellationToken)
		{
			await _unitOfWork.OwnerRepository.CreateAsync(owner);
			await _unitOfWork.SaveAsync(); 
		}

		public async Task<IEnumerable<Owner>> GetAllOwnersAsync()
		{
			return await _unitOfWork.OwnerRepository.GetAllAsync();
		}

		public async Task UpdateOwnerAsync(Owner owner)
		{
			_unitOfWork.OwnerRepository.UpdateAsync(owner);
			await _unitOfWork.SaveAsync(); 
		}

		public async Task DeleteOwnerAsync(int ownerId)
		{
			var owner = await _unitOfWork.OwnerRepository.GetById(ownerId);
			if (owner != null)
			{
				_unitOfWork.OwnerRepository.DeleteAsync(owner);
				await _unitOfWork.SaveAsync(); 
			}
		}
	}

}
