using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using DataAccess.Repositories.Impl;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Serv
{
	public class AuditLogsService : IAuditLogsService
	{
		private readonly IAuditLogsRepository _auditLogsRepository;

		public AuditLogsService(IAuditLogsRepository auditLogsRepository)
		{
			_auditLogsRepository = auditLogsRepository;
		}

		public async Task<(IEnumerable<AuditLogResponseModel> Logs, int TotalCount)> GetPagedAsync(int page, int pageSize)
		{
			var (logs, totalCount) = await _auditLogsRepository.GetPagedAsync(page, pageSize);
			
			var mappedLogs = logs.Select(l => new AuditLogResponseModel
			{
				AuditLogId = l.AuditLogId,
				UserId = l.UserId,
				Action = l.Action,
				Entity = l.Entity,
				KeyValues = l.KeyValues,
				OldValues = l.OldValues,
				NewValues = l.NewValues,
				CreatedAt = l.CreatedAt,
				User = l.User != null ? new AuditLogUserDto
				{
					UserName = l.User.Username,
					Email = l.User.Email
				} : null
			});

			return (mappedLogs, totalCount);
		}
	}
}
