using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Impl
{
	public interface IAuditLogsService
	{
		Task<(IEnumerable<AuditLogResponseModel> Logs, int TotalCount)> GetPagedAsync(int page, int pageSize);
		Task LogActionAsync(int? userId, string action, string entity, string keyValues = "{}", string oldValues = "{}", string newValues = "{}");
	}
}
