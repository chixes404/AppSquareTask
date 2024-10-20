using AppSquareTask.Application.Dtos;
using AppSquareTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.IServices
{
	public interface IBoatService
	{

		Task<Boat> GetBoatByIdAsync(int boatId);
		Task<IEnumerable<ResponseBoatDto>> GetBoatsByOwnerAsync(int ownerId); // New method
		Task<Boat> CreateBoatAsync(Boat boat); // Return Boat after creation
		Task<Boat> UpdateBoatAsync(Boat boat); // Return updated Boat
		Task DeleteBoatAsync(int boatId);

		Task<bool> RejectBoatAsync(int boatId);
		Task<bool> ApproveBoatAsync(int boatId);

	}
}
