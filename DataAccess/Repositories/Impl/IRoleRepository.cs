using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Impl
{
	public interface IRoleRepository : IBaseRepository<Role>
	{

		Task<Role> GetRoleNameAsync(string roleName);
		Task<bool> CheckExitNameRoleAsync(string roleName);
		Task<bool> DeleteRoleAsync(int roleId);
	}
}
