using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Impl
{
	public interface ISqlDataAccess
	{
		// Dùng cho Insert lấy ID, hoặc đếm số lượng (COUNT)
		Task<T> ExecuteSalarAsync<T>(string storedProcedure, object parameters = null);

		Task<T> ExecuteQuerySingleAsync<T>(string storedProcedure, object parameters = null);

		// Dùng cho Select danh sách
		Task<IEnumerable<T>> QueryAsync<T>(string storedProcedure, object parameters = null);

		// Dùng cho Select 1 dòng chi tiết
		Task<T> QueryFirstOrDefaultAsync<T>(string storedProcedure, object parameters = null);

		// Dùng cho Update, Delete (trả về số dòng bị ảnh hưởng)
		Task<int> ExecuteAsync(string storedProcedure, object parameters = null);

		// đọc nhiều bảng
		Task<T> QueryMultipleAsync<T>(string storedProcedure, Func<SqlMapper.GridReader, Task<T>> mapFunc, object parameters = null);

	}
}
