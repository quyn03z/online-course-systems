using Azure;
using DataAccess.Models.PageResultModel;
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
			return await _dbSet.AnyAsync(x => x.Email == email);
		}

		public async Task<bool> ExistsByUserNameAsync(string userName)
		{
			return await _dbSet.AnyAsync(x => x.Username == userName);
		}

		public async Task<PagedResults<User>> GetAllUserAdminPagedAsync(int page, int pageSize, string? search = null)
		{
			var query = _dbSet.Include(r => r.Role).AsQueryable();
			if (!string.IsNullOrWhiteSpace(search))
			{
				var term = search.Trim().ToLower();
				query = query.Where(u => u.Username.ToLower().Contains(term) || u.Email.ToLower().Contains(term));
			}
			return await query
						.OrderByDescending(u => u.UserId)
						.ToPagedListAsync(page, pageSize);
		}

		public async Task<int> GetTotalsUser()
		{
			return await _dbSet.CountAsync();
		}

		public async Task<User?> GetUserByEmail(string email)
		{
			return await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
		}

		public async Task<User> GetUserByIdAsync(int userId)
		{
			return await _dbSet.Include(r => r.Role).FirstOrDefaultAsync(x => x.UserId == userId);
		}

		public async Task<User?> GetUserByUserNameAsync(string userName)
		{
			return await _dbSet
				.Include(r => r.Role)
				.FirstOrDefaultAsync(x => x.Username == userName);
		}





	}
}
