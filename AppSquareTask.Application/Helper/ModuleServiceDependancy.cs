using AppSquareTask.Application.IServices;
using AppSquareTask.Application.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.Helper
{
	

	public static class ModuleServiceDependancy
	{
		public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
		{

			// Register Entity services

			services.AddScoped<IEmailService, EmailService>();
			services.AddScoped<INotificationService, NotificationService>();
			services.AddScoped<IBookingService, BookingService>();
		    services.AddScoped<ITripService, TripService>();
			services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<ICustomerService, CustomerService>();
			services.AddScoped<IBoatService, BoatService>();
			services.AddScoped<IWalletService, WalletService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<UserService>();
			return services;
		}
	}
}
