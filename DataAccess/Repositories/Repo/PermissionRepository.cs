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

			if (user == null) return new List<string>();

			var rolePermissions = await _context.RolePermissions
				.Where(rp => rp.RoleId == user.RoleId)
				.Include(rp => rp.Permission)
				.Select(rp => rp.Permission.Name)
				.ToListAsync();

			var userPermissions = await _context.UserPermissions
				.Where(up => up.UserId == userId)
				.Include(up => up.Permission)
				.Select(up => up.Permission.Name)
				.ToListAsync();

			return rolePermissions.Union(userPermissions).ToList();
		}

		// Trả về Permission objects (có id) để frontend có thể pre-check checkbox
		public async Task<List<Permission>> GetUserPermissionsWithIdAsync(int userId)
		{
			var user = await _context.Users
				.Include(u => u.Role)
				.FirstOrDefaultAsync(u => u.UserId == userId);

			if (user == null) return new List<Permission>();

			var rolePermissions = await _context.RolePermissions
				.Where(rp => rp.RoleId == user.RoleId)
				.Include(rp => rp.Permission)
				.Select(rp => rp.Permission)
				.ToListAsync();

			var userPermissions = await _context.UserPermissions
				.Where(rp => rp.UserId == user.UserId)
				.Include(rp => rp.Permission)
				.Select(rp => rp.Permission)
				.ToListAsync();

			return rolePermissions.Union(userPermissions).ToList();
		}

		public async Task<bool> HasPermissionAsync(int userId, string permissionName)
		{
			var permissions = await GetUserPermissionsAsync(userId);
			return permissions.Contains(permissionName);
		}

		public async Task UpdateUserPermissionsAsync(int userId, IEnumerable<int> permissionIds)
		{
			// Xóa các quyền cũ
			var oldPermissions = await _context.UserPermissions.Where(up => up.UserId == userId).ToListAsync();
			_context.UserPermissions.RemoveRange(oldPermissions);

			// Thêm các quyền mới
			if (permissionIds != null && permissionIds.Any())
			{
				var newPermissions = permissionIds.Select(pid => new UserPermission
				{
					UserId = userId,
					PermissionId = pid
				});
				await _context.UserPermissions.AddRangeAsync(newPermissions);
			}

			await _context.SaveChangesAsync();
		}
	}
}
