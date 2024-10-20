using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Data.Models
{
	[Table(name: "Roles")]
	public partial class Role : IdentityRole<Guid>
	{

		public Role()
		{
			Users = new List<ApplicationUser>();
		}

		[InverseProperty("Roles")] // Make sure this matches the correct navigation property in User
		public virtual ICollection<ApplicationUser> Users { get; set; }
		public virtual ICollection<IdentityUserRole<Guid>> UserRoles { get; set; } = new List<IdentityUserRole<Guid>>();

	}
}
