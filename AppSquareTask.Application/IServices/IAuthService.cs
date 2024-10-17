using AppSquareTask.Application.Dtos;
using AppSquareTask.Application.MediatrHandelr.Auth.CustomerRegister;
using AppSquareTask.Application.MediatrHandelr.Auth.Login;
using AppSquareTask.Application.MediatrHandelr.Auth.Register;
using AppSquareTask.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.IServices
{
	public interface IAuthService
	{

		Task<AuthResponse> OwnerRegisterAsync(OwnerRegisterCommand registerModel);
		Task<AuthResponse> CustomerRegisterAsync(CustomerRegisterCommand registerModel);

		Task<AuthResponse> LoginAsync(LoginCommand model);
	}
}
