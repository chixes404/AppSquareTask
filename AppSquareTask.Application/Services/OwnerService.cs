using AppSquareTask.Application.IServices;
using AppSquareTask.Core.IRepositories;
using AppSquareTask.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.Services
{
	public class OwnerService : IOwnerService
	{
		private readonly IUnitOfWork _unitOfWork;

		public OwnerService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Owner> GetOwnerByIdAsync(int ownerId)
		{
			// Use Unit of Work's OwnerRepository to fetch the owner by ID
			return await _unitOfWork.OwnerRepository.GetById(ownerId);
		}

		public async Task CreateOwnerAsync(Owner owner, CancellationToken cancellationToken)
		{
			// Add the new owner and save changes through Unit of Work
			await _unitOfWork.OwnerRepository.CreateAsync(owner);
			await _unitOfWork.SaveAsync(); // Save the changes in a single transaction
		}

		public async Task<IEnumerable<Owner>> GetAllOwnersAsync()
		{
			// Retrieve all owners using Unit of Work's OwnerRepository
			return await _unitOfWork.OwnerRepository.GetAllAsync();
		}

		public async Task UpdateOwnerAsync(Owner owner)
		{
			// Update the owner and save the changes
			_unitOfWork.OwnerRepository.UpdateAsync(owner);
			await _unitOfWork.SaveAsync(); // Commit the update
		}

		public async Task DeleteOwnerAsync(int ownerId)
		{
			// Get the owner by ID and delete them
			var owner = await _unitOfWork.OwnerRepository.GetById(ownerId);
			if (owner != null)
			{
				_unitOfWork.OwnerRepository.DeleteAsync(owner);
				await _unitOfWork.SaveAsync(); // Commit the delete action
			}
		}
	}

}
