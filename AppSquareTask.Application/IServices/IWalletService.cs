using AppSquareTask.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.IServices
{
	public interface IWalletService
	{


		Task<bool> Pay(Guid userId, decimal amount);


		Task<Wallet> GetWalletByCustomerIdAsync(Guid customerId);




	}
}
