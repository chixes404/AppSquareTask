using AppSquareTask.Core.Middlewares;
using AppSquareTask.Core.Responses;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core
{
	public static class ModuleCoreDependencies
	{
		public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
		{
			// Configuration of MediatR
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

			// Configuration of AutoMapper
			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			// Register FluentValidation
			services.AddFluentValidationAutoValidation()
				.AddFluentValidationClientsideAdapters()
				.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

			// Register ValidationBehavior
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

			services.AddTransient<ApiResponseHandler>();
			return services;
		}
	}

}
