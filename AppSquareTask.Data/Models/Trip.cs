﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSquareTask.Data.Models.Common;

namespace AppSquareTask.Data.Models
{
    public class Trip : BaseEntity
	{

		public string Name { get; set; }
		public string Description { get; set; }
		public decimal PricePerPerson { get; set; }
		public int Capacity { get; set; }
		public DateTime MaxCancellationPeriod { get; set; }
		public Status Status { get; set; } = Status.Pending; // "Pending", "Approved", "Rejected"
		public int OwnerId { get; set; }
		public Owner Owner { get; set; } 

		public int BoatId { get; set; }
		public Boat Boat { get; set; }
		public ICollection<TripBooking> Bookings { get; set; }
	}



}
