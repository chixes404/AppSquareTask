﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSquareTask.Data.Models.Common;

namespace AppSquareTask.Data.Models
{
    public class BoatBooking : BaseEntity
	{
		public int BoatId { get; set; }
		public int CustomerId { get; set; }
		public decimal Price { get; set; }
		public string Purpose { get; set; } 
		public DateTime BookingDate { get; set; }
		public bool IsPaid { get; set; }
		public bool IsCanceled { get; set; } = false;
		public DateTime? CancellationDate { get; set; }
		public bool IsRefunded { get; set; } = false;
		public Boat Boat { get; set; }
		public Customer Customer { get; set; } // The customer booking the boat


	}
}
