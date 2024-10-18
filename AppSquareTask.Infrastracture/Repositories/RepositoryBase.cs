using AppSquareTask.Core.IRepositories;
using AppSquareTask.Core.Models.Common;
using AppSquareTask.Infrastracture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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

		public IQueryable<T> Query()
		{
			return RepositoryContext.Set<T>().AsQueryable();
		}

		public async Task<T> GetById(int id)
		{
			return await RepositoryContext.Set<T>()
									  .FindAsync(id);
		}

		public IQueryable<T> Find(
		Expression<Func<T, bool>> predicate,
		Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
		{
			IQueryable<T> query = RepositoryContext.Set<T>();

			if (include != null)
			{
				query = include(query);
			}

			return query.Where(predicate);
		}



		public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
		{
			return RepositoryContext.Set<T>().Where(expression).AsNoTracking();
		}

		public async Task<IEnumerable<T>> FindAsync(
	  Expression<Func<T, bool>> predicate,
	  Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
	  CancellationToken cancellationToken = default)
		{
			IQueryable<T> query = RepositoryContext.Set<T>();

			if (include != null)
			{
				query = include(query);
			}

			return await query.Where(predicate).ToListAsync(cancellationToken);
		}


		public async Task<IEnumerable<T>> FindBySpecificationAsync(
	ISpecification<T> specification,
	CancellationToken cancellationToken = default)
		{
			IQueryable<T> query = RepositoryContext.Set<T>();

			if (specification.Criteria != null)
			{
				query = query.Where(specification.Criteria);
			}

			foreach (var include in specification.Includes)
			{
				query = include(query);
			}

			if (specification.OrderBy != null)
			{
				query = specification.OrderBy(query);
			}

			if (specification.Skip.HasValue)
			{
				query = query.Skip(specification.Skip.Value);
			}

			if (specification.Take.HasValue)
			{
				query = query.Take(specification.Take.Value);
			}

			return await query.ToListAsync(cancellationToken);
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



		public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
		{
			return await RepositoryContext.Set<T>().AnyAsync(predicate);
		}

		public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
		{
			return await RepositoryContext.Set<T>().CountAsync(predicate);
		}



	
		}

}
