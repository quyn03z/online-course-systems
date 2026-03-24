using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Impl
{
	public interface IAuditLogsRepository : IBaseRepository<AuditLog>
	{
		Task<(IEnumerable<AuditLog> Logs, int TotalCount)> GetPagedAsync(int page, int pageSize, string? search = null);
		Task<AuditLog?> GetByIdAsync(long id);
	}
}
