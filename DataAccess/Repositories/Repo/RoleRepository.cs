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
	public class RoleRepository : BaseRepository<Role>, IRoleRepository
	{
		public RoleRepository(OCMSMSFContext context) : base(context)
		{
		}

		public async Task<bool> CheckExitNameRoleAsync(string roleName)
		{
			var role = await _dbSet.FirstOrDefaultAsync(x => x.RoleName.ToLower() == roleName.ToLower());
			if (role == null) return true;
			return false;
		}

		public async Task<bool> DeleteRoleAsync(int roleId)
		{
			var role = await _dbSet.Include(x => x.Users).FirstOrDefaultAsync(x => x.Id == roleId);
			if (role == null) return false;

			if (role.Users.Any())
			{
				return false;
			}

			_dbSet.Remove(role);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<Role> GetRoleNameAsync(string roleName)
		{
			return await _dbSet.FirstOrDefaultAsync(r => r.RoleName == roleName);
		}
	}
}
