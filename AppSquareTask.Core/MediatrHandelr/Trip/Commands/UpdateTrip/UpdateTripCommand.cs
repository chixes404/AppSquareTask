﻿using AppSquareTask.Core.Responses;
using AppSquareTask.Data.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.MediatrHandelr.Trip.Commands.UpdateTrip
{
	public class UpdateTripCommand : IRequest<ApiResponse<TripDto>>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal PricePerPerson { get; set; }
		public int Capacity { get; set; }
		public DateTime MaxCancellationPeriod { get; set; }
		public Status Status { get; set; } 
	}
}
