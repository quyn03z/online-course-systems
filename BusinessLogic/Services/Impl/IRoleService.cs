using DataAccess.Models.RoleModel;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Impl
{
	public interface IRoleService
	{
		Task<IEnumerable<RoleResponseModel>> GetAllsRoleAsync();
		Task<string> CreateRoleAsync(RoleRequestModel roleRequestModel);
		Task<string> UpdateRoleAsync(RoleRequestModel roleRequestModel,int roleId);
		Task<string> DeleteRoleAsync(int roleId);

	}
}
