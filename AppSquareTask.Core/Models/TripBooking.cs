using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.Models
{
	public class TripBooking : BaseEntity
	{
		public int TripId { get; set; }
		public Guid UserId { get; set; }
		public int NumberOfParticipants { get; set; }
		public decimal TotalPrice { get; set; }
		public bool IsCanceled { get; set; } = false;
		public DateTime BookingDate { get; set; }
		public DateTime? CancellationDate { get; set; }
		public ApplicationUser Customer { get; set; }
		public Trip Trip { get; set; }


	}
}
