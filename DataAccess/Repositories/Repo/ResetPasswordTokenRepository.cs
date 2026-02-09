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
	public class ResetPasswordTokenRepository : BaseRepository<ResetPasswordToken>, IResetPasswordTokenRepository
	{
		public ResetPasswordTokenRepository(OCMSMSFContext context) : base(context)
		{
		}

		public async Task<ResetPasswordToken> GetByTokenAsync(string token)
		{
			return await _context.ResetPasswordTokens
				.Include(u => u.User)
				.Where(x => x.ResetToken == token && !x.IsUsed)
				.FirstOrDefaultAsync();
		}

		public async Task RevokeResetTokensAsync(int userId)
		{
			await _context.ResetPasswordTokens
				.Where(x => x.UserId == userId && !x.IsUsed)
				.ExecuteUpdateAsync(x => x.SetProperty(x => x.IsUsed, true));
		}
	}
}
