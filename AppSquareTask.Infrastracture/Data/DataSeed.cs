using AppSquareTask.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Infrastracture.Data
{
	public partial class ApplicationDbContext
	{
		partial void OnModelCreatingPartialDataSeed(ModelBuilder modelBuilder)
		{
		


			modelBuilder.Entity<Role>().HasData(
				new { Id = new Guid("bdebdb78-4dc9-40ac-b319-f4e5e7bc3503"), Name = "Admin", NormalizedName = "ADMIN" },
				new { Id = new Guid("b4018cb5-755e-468b-9802-a9917c37730e"), Name = "Owner", NormalizedName = "OWNER" },

				new { Id = new Guid("ee30e20d-5851-4f96-bc13-6aa7c73ce07c"), Name = "Customer", NormalizedName = "CUSTOMER" }



			);

		

		
		}
	}


}
