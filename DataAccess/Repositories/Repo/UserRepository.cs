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
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		public UserRepository(OCMSMSFContext context) : base(context)
		{
		}

		public async Task<bool> ExistsByEmailAsync(string email)
		{
			return await _dbSet.AllAsync(x => x.Email == email);
		}

		public async Task<bool> ExistsByUserNameAsync(string userName)
		{
			return await _dbSet.AllAsync(x => x.Equals(userName));
		}

		public async Task<User?> GetUserByEmail(string email)
		{
			return await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
		}

		public async Task<User?> GetUserByUserNameAsync(string userName)
		{
			return await _dbSet
				.Include(r => r.Role)
				.FirstOrDefaultAsync(x => x.Username == userName);
		}


	}
}
