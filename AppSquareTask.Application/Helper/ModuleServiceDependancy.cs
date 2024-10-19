using AppSquareTask.Application.IServices;
using AppSquareTask.Application.MediatrHandelr.Auth.OwnerRegister;
using AppSquareTask.Application.Responses;
using AppSquareTask.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AppSquareTask.Application.Helper
{


	public static class ModuleServiceDependancy
	{
		public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
		{

			// Register Entity services
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
			services.AddScoped<IEmailService, EmailService>();
			services.AddScoped<IOwnerService, OwnerService>();
			services.AddScoped<INotificationService, NotificationService>();
			services.AddScoped<ITripService, TripService>();
			services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<ICustomerService, CustomerService>();
			services.AddScoped<IBoatService, BoatService>();
			services.AddScoped<IWalletService, WalletService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<UserService>();

			return services;




		}



	}
}
