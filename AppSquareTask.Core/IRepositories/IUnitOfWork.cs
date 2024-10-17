using AppSquareTask.Core.Models;
using AppSquareTask.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.IRepositories
{
	public interface IUnitOfWork : IDisposable
	{
		IRepositoryBase<TEntity>? Repository<TEntity>() where TEntity : BaseEntity;
		IRepositoryBase<Owner> OwnerRepository { get; }
		IRepositoryBase<Wallet> WalletRepository { get; }
		IRepositoryBase<Customer> CustomerRepository { get; }
		IRepositoryBase<Trip> TripRepository { get; }
		IRepositoryBase<Boat> BoatRepository { get; }
		Task<int> SaveAsync();
	}
}
