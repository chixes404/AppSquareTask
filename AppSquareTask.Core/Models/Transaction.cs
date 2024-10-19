using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSquareTask.Core.Models.Common;

namespace AppSquareTask.Core.Models
{
    public class Transaction : BaseEntity
	{
		public decimal Amount { get; set; } 
		public int ? serviceId { get; set; }

		public string Type { get; set; } 
		public int WalletId { get; set; }
		public Wallet Wallet { get; set; } 
	}

}
