using AppSquareTask.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M = AppSquareTask.Core.Models;


namespace AppSquareTask.Infrastracture.Data
{
	public class ApplicationDbContext : IdentityDbContext<M.ApplicationUser, M.Role, Guid>
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		 : base(options)
		{

		}


		


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			// Additional model configuration if needed
		}



	}
}
