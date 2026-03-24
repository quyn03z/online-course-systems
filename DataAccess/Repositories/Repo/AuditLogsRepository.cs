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

		public async Task<(IEnumerable<AuditLog> Logs, int TotalCount)> GetPagedAsync(int page, int pageSize, string? search = null, DateTime? startDate = null, DateTime? endDate = null)
		{
			var query = _context.AuditLogs
				.Include(a => a.User)
				.AsQueryable();

			if (!string.IsNullOrWhiteSpace(search))
			{
				search = search.Trim().ToLower();
				query = query.Where(a => 
					(a.Action != null && a.Action.ToLower().Contains(search)) ||
					(a.Entity != null && a.Entity.ToLower().Contains(search)));
			}
			if (startDate.HasValue)
			{
				query = query.Where(x => x.CreatedAt >= startDate.Value);
			}
			if (endDate.HasValue)
			{
				var endOfDay = endDate.Value.Date.AddDays(1).AddTicks(-1);
				query = query.Where(x => x.CreatedAt <= endOfDay);
			}
			query = query.OrderByDescending(a => a.CreatedAt);

			var totalCount = await query.CountAsync();
			var logs = await query
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			return (logs, totalCount);
		}

		public async Task<AuditLog?> GetByIdAsync(long id)
		{
			return await _context.AuditLogs.FindAsync(id);
		}


	}
}
