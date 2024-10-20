using AppSquareTask.Application.Dtos;
using AppSquareTask.Core.MediatrHandelr.Trip.Commands.CreateTrip;
using AppSquareTask.Core.MediatrHandelr.Trip.Commands.UpdateTrip;
using AppSquareTask.Data.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M = AppSquareTask.Data.Models;


namespace AppSquareTask.Core.MediatrHandelr.Trip
{
	public class TripMapping : Profile
	{
		public TripMapping() {


			CreateMap<CreateTripCommand, M.Trip>();

			CreateMap<UpdateTripCommand, M.Trip>();

			CreateMap<M.Trip, TripDto>().ReverseMap();


			CreateMap<M.Trip, ResponseTripDto>();

			CreateMap<ResponseTripDto, TripDto>();

		}
		
			
	}
}
