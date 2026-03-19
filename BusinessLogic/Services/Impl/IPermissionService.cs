using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Impl
{
	public interface IPermissionService
	{

		Task<List<Permission>> GetAllPermissionsAsync();
		Task<List<Permission>> GetAllsPermissionsByRole(int roleId);
		Task<List<Permission>> GetUserPermissionsWithIdAsync(int userId);
		Task UpdateUserPermissionsAsync(int userId, IEnumerable<int> permissionIds);
		Task<List<string>> GetCurrentUserPermissionsAsync(int userId);
		Task<bool> HasPermissionAsync(int userId, string permissionName);

	}
}
