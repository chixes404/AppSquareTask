using AppSquareTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.IServices
{
	public interface IWalletService
	{


		Task<bool> Pay(Guid userId, decimal amount, int serviceId);


		Task<Wallet> GetWalletByCustomerIdAsync(Guid customerId);

	     Task<bool> RefundToWalletAsync(Guid userId, decimal amount);





	}
}
