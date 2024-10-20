using AppSquareTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.IServices
{
	public interface ICustomerService
	{
		Task<Customer> GetCustomerByIdAsync(int customerId);
		Task CreateCustomerAsync(Customer customer, CancellationToken cancellationToken);
		Task<IEnumerable<Customer>> GetAllCustomersAsync();
		Task UpdateCustomerAsync(Customer customer);
		Task DeleteCustomerAsync(int customerId);
	}
}
