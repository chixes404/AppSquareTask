﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Infrastracture.IRepositories
{

	public interface ISpecification<T>
	{
		// The criteria that the entity must satisfy
		Expression<Func<T, bool>> Criteria { get; }

		// Include related entities
		List<Func<IQueryable<T>, IQueryable<T>>> Includes { get; }

		// Sorting
		Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; }

		// Pagination
		int? Skip { get; }
		int? Take { get; }
	}
}
