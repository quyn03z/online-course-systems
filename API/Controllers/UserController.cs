using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using BusinessLogic.Services.Serv;
using DataAccess.Models.QuizzModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BusinessLogic.Models.User;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}
		[Authorize]
		[HttpPost("change-password")]
		public async Task<IActionResult> ChangePasswordAsync(ChangePassWordModel changePassWordModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(ApiResult<string>.Success(await _userService.ChangePasswordAsync(changePassWordModel)));
		}

		[Authorize]
		[HttpGet("user-profile")]
		public async Task<IActionResult> GetUserByIdAsync()
		{
			return Ok(ApiResult<UserResponseProfile>.Success(await _userService.GetUserByIdAsync()));
		}
	}
}
