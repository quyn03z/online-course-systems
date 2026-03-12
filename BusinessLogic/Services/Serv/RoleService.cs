using BusinessLogic.Exceptions;
using BusinessLogic.Services.Impl;
using DataAccess.Models.RoleModel;
using DataAccess.Repositories.Impl;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Exceptions;

namespace BusinessLogic.Services.Serv
{
	public class RoleService : IRoleService
	{

		private readonly IRoleRepository _roleRepository;

		public RoleService(IRoleRepository roleRepository)
		{
			_roleRepository = roleRepository;
		}

		public async Task<string> CreateRoleAsync(RoleRequestModel roleRequestModel)
		{
			if (!await _roleRepository.CheckExitNameRoleAsync(roleRequestModel.RoleName))
			{
				throw new BadRequestException("Role đã tồn tại trong hệ thống");
			}
			var newRole = new Role
			{
				RoleName = roleRequestModel.RoleName,
			};
			await _roleRepository.AddAsync(newRole);
			return "Thêm Role Thành Công";
		}

		public async Task<IEnumerable<RoleResponseModel>> GetAllsRoleAsync()
		{
			var allRole = await _roleRepository.GetAll();
			return allRole.Select(x => new RoleResponseModel
			{
				Id = x.Id,
				RoleName = x.RoleName,
			});
		}

		public async Task<string> UpdateRoleAsync(RoleRequestModel roleRequestModel, int roleId)
		{
			if (!await _roleRepository.CheckExitNameRoleAsync(roleRequestModel.RoleName))
			{
				throw new BadRequestException("Role đã tồn tại trong hệ thống");
			}
			var role = await _roleRepository.GetByIdAsync(roleId);
			role.RoleName = roleRequestModel.RoleName;
			await _roleRepository.UpdateAsync(role);
			return "Cập Nhật Role Thành Công";
		}

		public async Task<string> DeleteRoleAsync(int roleId)
		{
			var result = await _roleRepository.DeleteRoleAsync(roleId);
			if (!result)
			{
				throw new BadRequestException("Không thể xóa Role. Vui lòng kiểm tra xem Role có đang được sử dụng không.");
			}
			return "Xóa Role Thành Công";
		}
	}
}
