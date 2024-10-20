using AppSquareTask.Application.Dtos;
using AppSquareTask.Core.MediatrHandelr.Trip.Commands.CreateTrip;
using AppSquareTask.Core.MediatrHandelr.Trip.Commands.UpdateTrip;
using AppSquareTask.Core.MediatrHandelr.Trip;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSquareTask.Core.MediatrHandelr.Boat.Commands.CreateBoat;
using M = AppSquareTask.Data.Models;
using AppSquareTask.Data.Models;


namespace AppSquareTask.Core.MediatrHandelr.Boat
{
	public class BoatMapping : Profile
	{
		public BoatMapping()
		{



			CreateMap<M.Boat, ResponseBoatDto>();
			CreateMap<ResponseBoatDto, BoatDto>();



			CreateMap<CreateBoatCommand, M.Boat>();

			CreateMap<M.Boat, BoatDto>().ReverseMap();

		}


	}
}
