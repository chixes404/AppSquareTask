using AppSquareTask.Core.Models.Common;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace AppSquareTask.Core.IRepositories
{
	public interface IRepositoryBase<T> where T : BaseEntity
	{
		Task<IReadOnlyList<T>> GetAllAsync();
		Task<T> GetById(int id);

		IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
		Task<T> CreateAsync(T entity);
		Task<T> UpdateAsync(T entity);
		Task DeleteAsync(T entity);


		IQueryable<T> Query();

		// Query Operations
		Task<IEnumerable<T>> FindAsync(
			Expression<Func<T, bool>> predicate,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			CancellationToken cancellationToken = default);
		public IQueryable<T> Find(
			Expression<Func<T, bool>> predicate,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

		Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
		Task<int> CountAsync(Expression<Func<T, bool>> predicate);

	
		// Specification Pattern
		Task<IEnumerable<T>> FindBySpecificationAsync(
			ISpecification<T> specification,
			CancellationToken cancellationToken = default);
	}

}
