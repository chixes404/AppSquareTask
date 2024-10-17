﻿using AppSquareTask.Application.Responses;
using AppSquareTask.Core.Models;
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
		Task<PagedList<Boat>> GetAllBoatsPaginatedAsync(int pageNumber, int pageSize);
		Task<IEnumerable<Boat>> GetBoatsByOwnerAsync(int ownerId); // New method
		Task<Boat> CreateBoatAsync(Boat boat); // Return Boat after creation
		Task<Boat> UpdateBoatAsync(Boat boat); // Return updated Boat
		Task DeleteBoatAsync(int boatId);
	}
}
