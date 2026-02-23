using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BusinessLogic.Models.User;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly IUserService _userService;

		public AdminController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet("get-alls-user")]
		public async Task<IActionResult> GetAllUserAdmin()
		{
			return Ok(ApiResult<IEnumerable<UserResponseModel>>.Success(await _userService.GetAllUserAdmin()));
		}
	}
}
