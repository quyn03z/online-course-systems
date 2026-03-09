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
	public class AuditLogsRepository : BaseRepository<AuditLog>, IAuditLogsRepository
	{
		public AuditLogsRepository(OCMSMSFContext context) : base(context)
		{
		}

		public async Task<(IEnumerable<AuditLog> Logs, int TotalCount)> GetPagedAsync(int page, int pageSize)
		{
			var query = _context.AuditLogs
				.Include(a => a.User)
				.OrderByDescending(a => a.CreatedAt);

			var totalCount = await query.CountAsync();
			var logs = await query
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			return (logs, totalCount);
		}
	}
}
