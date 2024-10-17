using AppSquareTask.Core.IRepositories;
using AppSquareTask.Core.Models;
using AppSquareTask.Core.Models.Common;
using AppSquareTask.Infrastracture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Infrastracture.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;
		private IRepositoryBase<Owner> _ownerRepository;
		private IRepositoryBase<Customer> _customerRepository;
		private IRepositoryBase<Wallet> _walletRepository;
		private IRepositoryBase<Trip> _tripRepository;
		private IRepositoryBase<Boat> _boatRepository;
		private IRepositoryBase<BoatBooking> _boatBookingRepository;
		private IRepositoryBase<TripBooking> _tripBookingRepository;

		private Dictionary<string, object> _repositories = new Dictionary<string, object>();
		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
			
		}

		public IRepositoryBase<Owner> OwnerRepository
		{
			get { return _ownerRepository ??= new RepositoryBase<Owner>(_context); }
		}

		public IRepositoryBase<Customer> CustomerRepository
		{
			get { return _customerRepository ??= new RepositoryBase<Customer>(_context); }
		}
		public IRepositoryBase<Wallet> WalletRepository
		{
			get { return _walletRepository ??= new RepositoryBase<Wallet>(_context); }
		}



		public IRepositoryBase<Trip> TripRepository
		{
			get { return _tripRepository ??= new RepositoryBase<Trip>(_context); }
		}

		public IRepositoryBase<Boat> BoatRepository
		{
			get { return _boatRepository ??= new RepositoryBase<Boat>(_context); }
		}

		public IRepositoryBase<BoatBooking> BoatBookingRepository
		{
			get { return _boatBookingRepository ??= new RepositoryBase<BoatBooking>(_context); }
		}

		public IRepositoryBase<TripBooking> TripBookingRepository
		{
			get { return _tripBookingRepository ??= new RepositoryBase<TripBooking>(_context); }
		}
		public IRepositoryBase<TEntity>? Repository<TEntity>() where TEntity : BaseEntity
		{
			var Type = typeof(TEntity).Name;
			if (!_repositories.ContainsKey(Type))
			{
				var Repository = new RepositoryBase<TEntity>(_context);
				_repositories.Add(Type, Repository);
			}
			return _repositories[Type] as RepositoryBase<TEntity>;
		}

		public async Task<int> SaveAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}

}
