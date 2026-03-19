using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Impl
{
	public interface IPermissionRepository
	{
		Task<List<string>> GetUserPermissionsAsync(int userId);
		Task<List<Permission>> GetUserPermissionsWithIdAsync(int userId);
		Task<bool> HasPermissionAsync(int userId, string permissionName);
		Task<List<Permission>> GetAllPermissionsAsync();
		Task<List<Permission>> GetRolePermissionsAsync(int roleId);

	}
}
