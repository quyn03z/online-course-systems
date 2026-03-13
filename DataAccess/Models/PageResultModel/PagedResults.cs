using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.PageResultModel
{
	public class PagedResults<T>
	{
		public IEnumerable<T> Items { get; set; } = new List<T>();
		public int TotalItems { get; set; }
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public int TotalPages { get; set; }
		public bool HasPreviousPage => CurrentPage > 1;
		public bool HasNextPage => CurrentPage < TotalPages;


		public PagedResults<TNew> Map<TNew>(Func<T, TNew> selector)
		{
			return new PagedResults<TNew>
			{
				Items = this.Items.Select(selector).ToList(),
				TotalItems = this.TotalItems,
				CurrentPage = this.CurrentPage,
				PageSize = this.PageSize,
				TotalPages = this.TotalPages
			};
		}
	}
}
