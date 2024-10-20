using AppSquareTask.Application.IServices;
using AppSquareTask.Infrastracture.IRepositories;
using AppSquareTask.Data.Models;
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
			return await _unitOfWork.CustomerRepository.GetById(customerId);
		}

		public async Task CreateCustomerAsync(Customer customer, CancellationToken cancellationToken)
		{
			await _unitOfWork.CustomerRepository.CreateAsync(customer);
			await _unitOfWork.SaveAsync(); 
		}

		public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
		{
			return await _unitOfWork.CustomerRepository.GetAllAsync();
		}

		public async Task UpdateCustomerAsync(Customer customer)
		{
			_unitOfWork.CustomerRepository.UpdateAsync(customer);
			await _unitOfWork.SaveAsync(); 
		}

		public async Task DeleteCustomerAsync(int customerId)
		{
			var customer = await _unitOfWork.CustomerRepository.GetById(customerId);
			if (customer != null)
			{
				_unitOfWork.CustomerRepository.DeleteAsync(customer);
				await _unitOfWork.SaveAsync(); 
			}
		}
	}

}
