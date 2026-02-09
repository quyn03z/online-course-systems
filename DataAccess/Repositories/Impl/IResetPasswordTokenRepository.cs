using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Impl
{
	public interface IResetPasswordTokenRepository : IBaseRepository<ResetPasswordToken>
	{
		Task RevokeResetTokensAsync(int userId);

		Task<ResetPasswordToken> GetByTokenAsync(string token);
	}
}
