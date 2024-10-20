using AppSquareTask.Infrastracture.IRepositories;
using AppSquareTask.Infrastracture.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Infrastracture
{
	public static class ModuleInfrastructureDependencies
	{
		public static void AddInfrastructureDependencies(this IServiceCollection services)
		{
			services.AddSignalR();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
		}
	}
}
