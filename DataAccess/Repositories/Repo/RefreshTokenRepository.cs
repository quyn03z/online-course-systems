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
	public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
	{
		public RefreshTokenRepository(OCMSMSFContext context) : base(context)
		{
		}

		public async Task RevokeUserTokensAsync(int userId)
		{
			await _context.RefreshTokens
				.Where(x => x.UserId == userId && !x.IsRevoked)
				.ExecuteUpdateAsync(x => x.SetProperty(t => t.IsRevoked, true));					
		}

		public async Task<RefreshToken?> GetByTokenAsync(string token)
		{
			return await _context.RefreshTokens
				.Include(x => x.User)
					.ThenInclude(u => u.Role)
				.FirstOrDefaultAsync(x => x.Token == token && !x.IsRevoked && x.ExpiredAt > DateTime.UtcNow);
		}


	}
}
