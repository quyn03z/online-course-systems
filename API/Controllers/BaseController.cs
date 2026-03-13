using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public abstract class BaseController : ControllerBase
	{
		protected IEnumerable<string> GetModelErrors()
		{
			return ModelState.Values
				.SelectMany(v => v.Errors)
				.Select(e => e.ErrorMessage);
		}

		protected IActionResult ValidationError()
		{
			return BadRequest(ApiResult<string>.Failure(GetModelErrors()));
		}
	}
}
