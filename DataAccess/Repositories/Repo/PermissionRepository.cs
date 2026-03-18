using DataAccess.Repositories.Impl;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Repo
{
	public class PermissionRepository : IPermissionRepository
	{
		private readonly OCMSMSFContext _context;

		public PermissionRepository(OCMSMSFContext context)
		{
			_context = context;
		}

		public async Task<List<Permission>> GetAllPermissionsAsync()
		{
			return await _context.Permissions.ToListAsync();
		}

		public Task<List<Permission>> GetRolePermissionsAsync(int roleId)
		{
			throw new NotImplementedException();
		}

		public Task<List<string>> GetUserPermissionsAsync(int userId)
		{
			throw new NotImplementedException();
		}

		public Task<bool> HasPermissionAsync(int userId, string permissionName)
		{
			throw new NotImplementedException();
		}
	}
}
