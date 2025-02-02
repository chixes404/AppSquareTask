﻿using AppSquareTask.Application.Dtos;
using AppSquareTask.Application.IServices;

using AppSquareTask.Infrastracture.IRepositories;
using AppSquareTask.Data.Models;
using AppSquareTask.Infrastracture.Configuration;
using AppSquareTask.Infrastracture.Data;
using AppSquareTask.Application.Helper;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;

using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.Services
{
	public class AuthService : IAuthService
	{
		

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMapper _mapper; 
		private readonly JWT _jwt;
		private readonly RoleManager<Role> _roleManager;
		private readonly IUnitOfWork _unitOfWork;
		private readonly JwtTokenGenerator _jwtTokenGenerator;
		//private readonly EmailService _emailService;


		public AuthService(UserManager<ApplicationUser> userManager, JwtTokenGenerator jwtTokenGenerator, IUnitOfWork unitOfWork, IOptions<JWT> jwt, IMapper mapper, RoleManager<Role> roleManager)
		{
			_userManager = userManager;
			_jwt = jwt.Value;
			_mapper = mapper; // Initialize IMapper
			_roleManager = roleManager;
			_unitOfWork = unitOfWork;
			_jwtTokenGenerator = jwtTokenGenerator;
		}



		public async Task<AuthResponse> OwnerRegisterAsync(string username , string email , string password)
		{
			if (await _userManager.FindByEmailAsync(email) is not null)
			{
				return new AuthResponse { Message = "Email is already registered." };
			}


			var user = new ApplicationUser
			{
				UserName = username,
				Email = email,
				CreatedAt = DateTime.UtcNow
			};
			user.Status = Status.Pending;

			var result = await _userManager.CreateAsync(user, password);


			if (!result.Succeeded)
			{
				return new AuthResponse
				{
					Succeeded = false,
					Errors = result.Errors.Select(e => e.Description).ToList(),
					Message = "Registration failed. See errors for details."
				};
			}

			await _userManager.AddToRoleAsync(user, "Owner");  
			var wallet = new Wallet
			{
				UserId = user.Id,
				Balance=0,
				CreatedBy = user.Id

			};
			await _unitOfWork.WalletRepository.CreateAsync(wallet);


			var owner = new Owner
			{
				UserId = user.Id,
			
				CreatedBy = user.Id
			};
			await _unitOfWork.OwnerRepository.CreateAsync(owner);
			await _unitOfWork.SaveAsync();



			return new AuthResponse
			{
				Succeeded = true,
				Message = $"{user.UserName} registered successfully. Await admin approval."
			};
		}

		public async Task<AuthResponse> AdminRegister(string username, string email, string password)
		{
			if (await _userManager.FindByEmailAsync(email) is not null)
			{
				return new AuthResponse { Message = "Email is already registered." };
			}


			var user = new ApplicationUser
			{
				UserName = username,
				Email = email,
				CreatedAt = DateTime.UtcNow
			};
			user.Status = Status.Approved;

			var result = await _userManager.CreateAsync(user, password);


			if (!result.Succeeded)
			{
				return new AuthResponse
				{
					Succeeded = false,
					Errors = result.Errors.Select(e => e.Description).ToList(),
					Message = "Registration failed. See errors for details."
				};
			}

			await _userManager.AddToRoleAsync(user, "Admin");


			return new AuthResponse
			{
				Succeeded = true,
				Message = $"{user.UserName} registered successfully. "
			};
		}



		public async Task<AuthResponse> CustomerRegisterAsync(string username, string email, string password)
		{
			if (await _userManager.FindByEmailAsync(email) is not null)
			{
				return new AuthResponse { Message = "Email is already registered." };
			}


			var user = new ApplicationUser
			{
				UserName = username,
				Email = email,

			};
				user.Status = Status.Approved;
			var result = await _userManager.CreateAsync(user, password);

			if (!result.Succeeded)
			{
				return new AuthResponse
				{
					Succeeded = false,
					Errors = result.Errors.Select(e => e.Description).ToList(),
					Message = "Registration failed. See errors for details."
				};
			}

			await _userManager.AddToRoleAsync(user, "Customer");

			var wallet = new Wallet
			{
				UserId = user.Id,
				Balance = 0

			};
			await _unitOfWork.WalletRepository.CreateAsync(wallet);


			var customer = new Customer
			{
				UserId = user.Id,
			};


			await _unitOfWork.CustomerRepository.CreateAsync(customer);
			await _unitOfWork.SaveAsync();

			var token = _jwtTokenGenerator.CreateToken(user); 
			var tokenExpiration = DateTime.Now.AddHours(1); 


			return new AuthResponse
			{
				Succeeded = true,
				Message = $"{user.UserName} registered successfully.",
			    Token = token,
				TokenExpiration = tokenExpiration
			};
		}



		public async Task<AuthResponse> LoginAsync(string email , string password )
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return new AuthResponse { Succeeded = false, Message = "Invalid credentials." };
			}

			if (user.Status != Status.Approved)
			{
				return new AuthResponse { Succeeded = false, Message = "Your account is not approved yet." };
			}

			var passwordValid = await _userManager.CheckPasswordAsync(user, password);
			if (!passwordValid)
			{
				return new AuthResponse { Succeeded = false, Message = "Invalid credentials." };
			}

			var token = _jwtTokenGenerator.CreateToken(user); 
			var tokenExpiration = DateTime.Now.AddHours(1); 

			return new AuthResponse
			{
				Succeeded = true,
				Token = token,
				TokenExpiration = tokenExpiration,
				Message = "Login successful."
			};
		}

		public async Task<AuthResponse> AdminLoginAsync(string email, string password)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return new AuthResponse { Succeeded = false, Message = "Invalid credentials." };
			}

			if (user.Status != Status.Approved)
			{
				return new AuthResponse { Succeeded = false, Message = "Your account is not approved yet." };
			}

			var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

			if (!isAdmin)
			{
				return new AuthResponse { Succeeded = false, Message = "Admins only can login." };

			}


			var passwordValid = await _userManager.CheckPasswordAsync(user, password);
			if (!passwordValid)
			{
				return new AuthResponse { Succeeded = false, Message = "Invalid credentials." };
			}

			var token = _jwtTokenGenerator.CreateToken(user);
			var tokenExpiration = DateTime.Now.AddHours(1);

			return new AuthResponse
			{
				Succeeded = true,
				Token = token,
				TokenExpiration = tokenExpiration,
				Message = "Login successful."
			};
		}



	}
}
