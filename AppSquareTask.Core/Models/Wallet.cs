using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.Models
{
	public class Wallet : BaseEntity
	{
		public decimal Balance { get; set; } = 0; // Default balance
		public Guid UserId { get; set; }
		public ApplicationUser User { get; set; } // Navigation property for user
		public ICollection<Transaction> Transactions { get; set; } // Transactions related to this wallet
	}

}
