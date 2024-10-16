using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.Models
{
	public class BoatBooking : BaseEntity
	{
		public int BoatId { get; set; }
		public Guid UserId { get; set; }
		public string Purpose { get; set; } // Purpose of booking (e.g., "Private Event", "Fishing", etc.)
		public string AdditionalServices { get; set; } // E.g., catering, special crew, etc.
		public DateTime BookingDate { get; set; }
		public bool IsCanceled { get; set; } = false;
		public DateTime? CancellationDate { get; set; }

		public Boat Boat { get; set; }
		public ApplicationUser Customer { get; set; } // The customer booking the boat


	}
}
