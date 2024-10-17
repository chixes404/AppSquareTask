using AppSquareTask.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
	}

}
