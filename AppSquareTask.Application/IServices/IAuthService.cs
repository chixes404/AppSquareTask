using AppSquareTask.Application.Dtos;

using AppSquareTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.IServices
{
	public interface IAuthService
	{

		Task<AuthResponse> OwnerRegisterAsync(string username , string email , string password);
		Task<AuthResponse> CustomerRegisterAsync(string username, string email, string password);
		Task<AuthResponse> AdminRegister(string username, string email, string password);
		Task<AuthResponse> LoginAsync( string email, string password);
		Task<AuthResponse> AdminLoginAsync( string email, string password);
	}
}
