﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSquareTask.Core.Models.Common;

namespace AppSquareTask.Core.Models
{
    public class Boat : BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public int Capacity { get; set; }
		public Status Status { get; set; } = Status.Pending; // "Pending", "Approved", "Rejected"
		public int OwnerId { get; set; }
		public Owner Owner { get; set; } 
		public ICollection<Trip> Trips { get; set; }
		public ICollection<BoatBooking> BoatBookings { get; set; } 
	}



}

