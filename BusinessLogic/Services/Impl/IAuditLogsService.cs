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
		Task<(IEnumerable<AuditLogResponseModel> Logs, int TotalCount)> GetPagedAsync(int page, int pageSize, string? search = null);
		Task LogActionAsync(int? userId, string action, string entity, string keyValues = "{}", string oldValues = "{}", string newValues = "{}", string? ipAddress = null, string? userAgent = null, int? durationMs = null);
		Task<bool> DeleteAsync(long id);
	}
}
