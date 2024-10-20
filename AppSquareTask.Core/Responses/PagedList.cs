using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.Responses
{
	public class PagedList<T>
	{
		public IReadOnlyList<T> Items { get; }
		public int PageNumber { get; }
		public int PageSize { get; }
		public int TotalCount { get; }
		public int TotalPages { get; }

		public PagedList(IEnumerable<T> items, int pageNumber, int pageSize, int totalCount)
		{
			PageNumber = pageNumber;
			PageSize = pageSize;
			TotalCount = totalCount;
			TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
			Items = items.ToList();
		}

		public bool HasPreviousPage => PageNumber > 1;
		public bool HasNextPage => PageNumber < TotalPages;
	}
}
