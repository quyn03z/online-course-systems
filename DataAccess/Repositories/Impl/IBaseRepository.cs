using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Impl
{
	public interface IBaseRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAll();
		Task<T> AddAsync(T entity);
		Task<T> UpdateAsync(T entity);
		Task DeleteAsync(T entity);
		Task<T?> GetByIdAsync(int id);

	}
}
