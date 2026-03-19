using BusinessLogic.Claims;
using BusinessLogic.Services.Impl;
using DataAccess.Repositories.Impl;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Serv
{
	public class PermissionService : IPermissionService
	{
		private readonly IPermissionRepository _permissionRepository;
		private readonly IClaimService _claimService;

		public PermissionService(IPermissionRepository permissionRepository, IClaimService claimService)
		{
			_permissionRepository = permissionRepository;
			_claimService = claimService;
		}

		public async Task<List<Permission>> GetAllPermissionsAsync()
		{
			return await _permissionRepository.GetAllPermissionsAsync();
		}

		public async Task<List<Permission>> GetAllsPermissionsByRole(int roleId)
		{
			return await _permissionRepository.GetRolePermissionsAsync(roleId);
		}

		public async Task<List<string>> GetCurrentUserPermissionsAsync(int userId)
		{
			return await _permissionRepository.GetUserPermissionsAsync(userId);
		}

		public async Task<List<Permission>> GetUserPermissionsWithIdAsync(int userId)
		{
			return await _permissionRepository.GetUserPermissionsWithIdAsync(userId);
		}

		public async Task<bool> HasPermissionAsync(int userId,string permissionName)
		{
			return await _permissionRepository.HasPermissionAsync(userId, permissionName);
		}
	}
}
