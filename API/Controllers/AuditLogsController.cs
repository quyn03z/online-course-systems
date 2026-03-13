using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AuditLogsController : BaseController
	{
		private readonly IAuditLogsService _auditLogsService;

		public AuditLogsController(IAuditLogsService auditLogsService)
		{
			_auditLogsService = auditLogsService;
		}

		[HttpGet]
		public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
		{
			var (logs, totalCount) = await _auditLogsService.GetPagedAsync(page, pageSize);
			return Ok(ApiResult<object>.Success(new { Logs = logs, TotalCount = totalCount }));
		}
	}
}
