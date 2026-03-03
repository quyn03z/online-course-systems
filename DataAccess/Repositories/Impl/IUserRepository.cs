using DataAccess.Models.PageResultModel;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Impl
{
	public interface IUserRepository : IBaseRepository<User>
	{
		Task<User?> GetUserByEmail(string email);
		Task<User?> GetUserByUserNameAsync(string userName);
		Task<bool> ExistsByEmailAsync(string email);
		Task<bool> ExistsByUserNameAsync(string userName);
		Task<PagedResults<User>> GetAllUserAdminPagedAsync(int page, int pageSize);
		Task<User> GetUserByIdAsync(int userId);
	}
}
