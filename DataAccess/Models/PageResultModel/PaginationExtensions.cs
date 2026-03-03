using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.PageResultModel
{
	public static class PaginationExtensions
	{
		public static async Task<PagedResults<T>> ToPagedListAsync<T>(
		this IQueryable<T> query,
		int pageNumber,
		int pageSize)
		{
			// 1. Bảo vệ đầu vào để tránh lỗi tính toán
			pageNumber = pageNumber < 1 ? 1 : pageNumber;
			pageSize = pageSize < 1 ? 10 : pageSize;

			// 2. Đếm tổng số bản ghi trước khi phân trang
			var totalItems = await query.CountAsync();

			// 3. Tính tổng số trang (làm tròn lên. VD: 11 items / 10 = 1.1 => 2 trang)
			var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

			// 4. Lấy dữ liệu trang hiện tại
			var items = await query
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			return new PagedResults<T>
			{
				Items = items,
				TotalItems = totalItems,
				CurrentPage = pageNumber,
				PageSize = pageSize,
				TotalPages = totalPages
			};
		}
	}
}
