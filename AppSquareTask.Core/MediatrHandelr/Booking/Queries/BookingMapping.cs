using AppSquareTask.Application.Dtos;
using AppSquareTask.Core.MediatrHandelr.Boat.Commands.CreateBoat;
using AppSquareTask.Core.MediatrHandelr.Boat;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSquareTask.Data.Models;

namespace AppSquareTask.Core.MediatrHandelr.Booking.Queries
{
	public class BookingMapping : Profile
	{
		public BookingMapping()
		{




			CreateMap<BoatBookingDto, BoatBooking>().ReverseMap();

			CreateMap<TripBookingDto, TripBooking>().ReverseMap();

		}


	}
}
