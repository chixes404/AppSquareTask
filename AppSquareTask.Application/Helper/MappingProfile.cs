using AppSquareTask.Application.MediatrHandelr.Auth.CustomerRegister;
using AppSquareTask.Application.MediatrHandelr.Auth.Register;
using AppSquareTask.Core.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.Helper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<OwnerRegisterCommand, ApplicationUser>();
			CreateMap<CustomerRegisterCommand, ApplicationUser>();




		}



	}
}
