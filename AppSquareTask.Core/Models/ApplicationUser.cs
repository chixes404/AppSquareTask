using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.Models
{
	public class ApplicationUser : IdentityUser<Guid>
	{
		public Status Status { get; set; } = Status.Pending; // "Pending", "Approved", "Rejected"

		public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
		public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
	}
}
