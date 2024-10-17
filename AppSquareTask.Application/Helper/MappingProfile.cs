using AppSquareTask.Application.MediatrHandelr.Auth.CustomerRegister;
using AppSquareTask.Application.MediatrHandelr.Auth.Register;
using AppSquareTask.Application.MediatrHandelr.Boat;
using AppSquareTask.Application.MediatrHandelr.Boat.Commands.CreateBoat;
using AppSquareTask.Application.MediatrHandelr.Trip;
using AppSquareTask.Application.MediatrHandelr.Trip.Commands.CreateTrip;
using AppSquareTask.Application.MediatrHandelr.Trip.Commands.UpdateTrip;
using AppSquareTask.Core.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppSquareTask.Application.Helper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<OwnerRegisterCommand, ApplicationUser>();
			CreateMap<CustomerRegisterCommand, ApplicationUser>();



			CreateMap<CreateTripCommand,Trip>();
			CreateMap<UpdateTripCommand, Trip>();
			CreateMap<Trip, TripDto>().ReverseMap();


			CreateMap<CreateBoatCommand, Boat>();
			CreateMap<Boat, BoatDto>().ReverseMap();


		}



	}
}
