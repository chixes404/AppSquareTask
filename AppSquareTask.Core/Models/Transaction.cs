using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.Models
{
	public class Transaction : BaseEntity
	{
		public decimal Amount { get; set; } // Positive for credits, negative for debits
		public string Type { get; set; } // "Credit" or "Debit"
		public int WalletId { get; set; }
		public Wallet Wallet { get; set; } // Navigation property for wallet
	}

}
