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
	public class CustomerService : ICustomerService
	{
		private readonly IUnitOfWork _unitOfWork;

		public CustomerService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Customer> GetCustomerByIdAsync(int customerId)
		{
			// Use Unit of Work's CustomerRepository to fetch the customer by ID
			return await _unitOfWork.CustomerRepository.GetById(customerId);
		}

		public async Task CreateCustomerAsync(Customer customer, CancellationToken cancellationToken)
		{
			// Add the new customer and save changes through Unit of Work
			await _unitOfWork.CustomerRepository.CreateAsync(customer);
			await _unitOfWork.SaveAsync(); // Save the changes in a single transaction
		}

		public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
		{
			// Retrieve all customers using Unit of Work's CustomerRepository
			return await _unitOfWork.CustomerRepository.GetAllAsync();
		}

		public async Task UpdateCustomerAsync(Customer customer)
		{
			// Update the customer and save the changes
			_unitOfWork.CustomerRepository.UpdateAsync(customer);
			await _unitOfWork.SaveAsync(); // Commit the update
		}

		public async Task DeleteCustomerAsync(int customerId)
		{
			// Get the customer by ID and delete them
			var customer = await _unitOfWork.CustomerRepository.GetById(customerId);
			if (customer != null)
			{
				_unitOfWork.CustomerRepository.DeleteAsync(customer);
				await _unitOfWork.SaveAsync(); // Commit the delete action
			}
		}
	}

}
