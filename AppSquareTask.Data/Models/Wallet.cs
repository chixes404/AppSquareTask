using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSquareTask.Data.Models.Common;

namespace AppSquareTask.Data.Models
{
    public class Wallet : BaseEntity
	{
		public decimal Balance { get; set; } = 0; // Default balance
	      public Guid UserId {  get; set; }
		public ICollection<Transaction> Transactions { get; set; } // Transactions related to this wallet
		public  virtual ApplicationUser User { get; set; } // Transactions related to this wallet
	}

}
