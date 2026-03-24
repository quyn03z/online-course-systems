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
		public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
		{
			var (logs, totalCount) = await _auditLogsService.GetPagedAsync(page, pageSize, search, startDate, endDate);
			return Ok(ApiResult<object>.Success(new { Logs = logs, TotalCount = totalCount }));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(long id)
		{
			var result = await _auditLogsService.DeleteAsync(id);
			return Ok(ApiResult<string>.Success("Xóa bản ghi thành công."));
		}
	}
}
