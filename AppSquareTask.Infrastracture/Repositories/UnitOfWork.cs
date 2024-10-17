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
