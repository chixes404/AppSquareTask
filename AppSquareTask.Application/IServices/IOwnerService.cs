using AppSquareTask.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.IServices
{
	public interface IOwnerService
	{
		Task<Owner> GetOwnerByIdAsync(int ownerId);
		Task CreateOwnerAsync(Owner owner, CancellationToken cancellationToken);
		Task<IEnumerable<Owner>> GetAllOwnersAsync();
		Task UpdateOwnerAsync(Owner owner);
		Task DeleteOwnerAsync(int ownerId);
	}
}
