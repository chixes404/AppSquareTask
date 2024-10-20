using AppSquareTask.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using M = AppSquareTask.Data.Models;


namespace AppSquareTask.Infrastracture.Data
{
	public partial class ApplicationDbContext : IdentityDbContext<M.ApplicationUser, M.Role, Guid>
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		 : base(options)
		{

		}



		public DbSet<ApplicationUser> Users { get; set; }
		public DbSet<Role> Roles { get; set; }

		public DbSet<Owner> Owners { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Boat> Boats { get; set; }
		public DbSet<Trip> Trips { get; set; }
		public DbSet<Transaction> Transactions { get; set; }
		public DbSet<TripBooking> TripBookings { get; set; }
		public DbSet<BoatBooking> BoatBookings { get; set; }




		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);



			

			// Define other relationships
			builder.Entity<Owner>()
				.HasMany(o => o.Boats)
				.WithOne(b => b.Owner)
				.HasForeignKey(b => b.OwnerId)
				.OnDelete(DeleteBehavior.Cascade); // Cascade delete here is fine if there are no conflicts

			builder.Entity<Customer>()
				.HasMany(c => c.BoatBookings)
				.WithOne(bb => bb.Customer)
				.HasForeignKey(bb => bb.CustomerId)
				.OnDelete(DeleteBehavior.Cascade);











			OnModelCreatingPartialDataSeed(builder);

		}


		partial void OnModelCreatingPartialDataSeed(ModelBuilder modelBuilder);

	}
}
