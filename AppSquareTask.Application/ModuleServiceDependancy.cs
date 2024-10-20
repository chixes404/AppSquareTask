using AppSquareTask.Application.Helper;
using AppSquareTask.Application.IServices;
using AppSquareTask.Application.Services;
using AppSquareTask.Infrastracture.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AppSquareTask.Application
{


    public static class ModuleServiceDependancy
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {

            // Register Entity services
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped< JwtTokenGenerator>();
            services.AddScoped<JWT>();
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
