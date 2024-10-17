using AppSquareTask.Core.IRepositories;
using AppSquareTask.Core.Models.Common;
using AppSquareTask.Infrastracture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Infrastracture.Repositories
{
	public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
	{
		protected ApplicationDbContext RepositoryContext { get; set; }
		public RepositoryBase(ApplicationDbContext repositoryContext)
		{
			RepositoryContext = repositoryContext;
		}
		public async Task<IReadOnlyList<T>> GetAllAsync()
		{

			return await RepositoryContext.Set<T>().AsNoTracking().ToListAsync();


		}


		public async Task<T> GetById(int id)
		{
			return await RepositoryContext.Set<T>()
									  .FindAsync(id);
		}


		public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
		{
			return RepositoryContext.Set<T>().Where(expression).AsNoTracking();
		}
		public async Task<T> CreateAsync(T entity)
		{
			await RepositoryContext.Set<T>().AddAsync(entity);
			return entity; // Return the created entity
		}

		public async Task<T> UpdateAsync(T entity)
		{
			RepositoryContext.Set<T>().Update(entity);
			return entity;
		}

		public async Task DeleteAsync(T entity)
		{
			RepositoryContext.Set<T>().Remove(entity);
		}


	}

}
