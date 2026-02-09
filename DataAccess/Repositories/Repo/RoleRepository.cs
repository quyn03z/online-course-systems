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

		public async Task<Role> GetRoleNameAsync(string roleName)
		{
			return await _dbSet.FirstOrDefaultAsync(r => r.RoleName == roleName);
		}
	}
}
