using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSquareTask.Core.Models.Common;

namespace AppSquareTask.Core.Models
{
    public class Wallet : BaseEntity
	{
		public decimal Balance { get; set; } = 0; // Default balance
		public int ownerId { get; set; }
		public int CustomerId { get; set; }
		public Customer customer { get; set; } // Navigation property for user
		public Owner owner { get; set; } // Navigation property for user
		public ICollection<Transaction> Transactions { get; set; } // Transactions related to this wallet
	}

}
