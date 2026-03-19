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

		public async Task<List<Permission>> GetRolePermissionsAsync(int roleId)
		{
			return await _context.RolePermissions
			.Where(rp => rp.RoleId == roleId)
			.Include(rp => rp.Permission)
			.Select(rp => rp.Permission)
			.ToListAsync();
		}

		public async Task<List<string>> GetUserPermissionsAsync(int userId)
		{
			var user = await _context.Users
		   .Include(u => u.Role)
		   .FirstOrDefaultAsync(u => u.UserId == userId);

			if (user == null)
				return new List<string>();

			var permissions = await _context.RolePermissions
				.Where(rp => rp.RoleId == user.RoleId)
				.Include(rp => rp.Permission)
				.Select(rp => rp.Permission.Name)
				.ToListAsync();

			return permissions;
		}

		// Trả về Permission objects (có id) để frontend có thể pre-check checkbox
		public async Task<List<Permission>> GetUserPermissionsWithIdAsync(int userId)
		{
			var user = await _context.Users
				.Include(u => u.Role)
				.FirstOrDefaultAsync(u => u.UserId == userId);

			if (user == null) return new List<Permission>();

			return await _context.UserPermissions
				.Where(rp => rp.UserId == user.UserId)
				.Include(rp => rp.Permission)
				.Select(rp => rp.Permission)
				.ToListAsync();
		}

		public async Task<bool> HasPermissionAsync(int userId, string permissionName)
		{
			var permissions = await GetUserPermissionsAsync(userId);
			return permissions.Contains(permissionName);
		}
	}
}
