using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.Models
{
	public class Owner : ApplicationUser
	{

		public Wallet Wallet { get; set; } // Associated wallet for payments
		public ICollection<Boat> Boats { get; set; } = new List<Boat>(); // Owned boats



	}
}
